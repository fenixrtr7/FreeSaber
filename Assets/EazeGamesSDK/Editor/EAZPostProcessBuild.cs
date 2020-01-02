using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IPHONE
using UnityEditor.iOS.Xcode;
#endif
using UnityEngine;

namespace EazeGamesSDK
{
    public class EAZPostProcessBuild : ScriptableObject
    {
        public DefaultAsset entitlementsFile;

        private const string EAZConfigPlist = "EAZConfig.plist";
        private const string eazConfigPath = "Assets/" + EAZConfigPlist;



        [PostProcessBuild]
        public static void OnPostProcessBuild( BuildTarget buildTarget, string path )
        {
#if UNITY_IPHONE

            string appIdentifier =
#if UNITY_2017_1_OR_NEWER
                Application.identifier;
#else
                Application.bundleIdentifier;
#endif

            // Go get pbxproj file
            string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

            PBXProject proj = new PBXProject();
            proj.ReadFromFile( projPath );

            string targetName = PBXProject.GetUnityTargetName();

            // This is the Xcode target in the generated project
            string targetGuid = proj.TargetGuidByName( targetName );

#if UNITY_2017_1_OR_NEWER
            proj.AddCapability( targetGuid, PBXCapabilityType.PushNotifications );
#else
                Debug.LogWarning( "AddCapability not supported. Enable PushNotifications capability manually in Xcode" );
#endif

            var dummy = CreateInstance<EAZPostProcessBuild>();
            var entitlementsFile = dummy.entitlementsFile;
            DestroyImmediate( dummy );
            if (entitlementsFile != null)
            {
                string entitlementPath = AssetDatabase.GetAssetPath( entitlementsFile );
                string entitlementFileName = appIdentifier + ".entitlements";
                //string entitlementFileName = Path.GetFileName(entitlementPath);
                string relativeDestination = targetName + "/" + entitlementFileName;
                FileUtil.ReplaceFile( entitlementPath, path + "/" + relativeDestination );
                // Add the pbx configs to include the entitlements files on the project
                proj.AddFile( relativeDestination, entitlementFileName );
                proj.SetBuildProperty( targetGuid, "CODE_SIGN_ENTITLEMENTS", relativeDestination );
            }

            // Copy plist from the project folder to the build folder
            FileUtil.ReplaceFile( eazConfigPath, path + "/" + EAZConfigPlist ); //used ReplaceFile to avoid errors when appending to existing project
            proj.AddFileToBuild( targetGuid, proj.AddFile( EAZConfigPlist, EAZConfigPlist ) );

            //Needed for EAZ IOS SDK, written on Swift
            proj.SetBuildProperty( targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES" );

            // Write PBXProject object back to the file
            proj.WriteToFile( projPath );

            // Get EAZConfig.plist
            PlistDocument eazPlist = new PlistDocument();
            eazPlist.ReadFromString( File.ReadAllText( eazConfigPath ) );
            string eazOwnerID = eazPlist.root["OWNER_ID"].AsString();

            // Get Info.plist
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString( File.ReadAllText( plistPath ) );
            // Get root
            PlistElementDict rootDict = plist.root;
            string buildKey = "UIBackgroundModes";
            PlistElementArray bgModesArray = rootDict[buildKey] == null ? rootDict.CreateArray( buildKey ) : rootDict[buildKey].AsArray();
            bgModesArray.AddString( "remote-notification" );

            //TODO: check build when appending existing project
            PlistElementArray urlTypes = rootDict["CFBundleURLTypes"] == null ? rootDict.CreateArray( "CFBundleURLTypes" ) : rootDict["CFBundleURLTypes"].AsArray();
            PlistElementDict dict = urlTypes.AddDict();
            dict.SetString( "CFBundleURLName", appIdentifier );
            dict.SetString( "CFBundleTypeRole", "Editor" );
            PlistElementArray array = dict.CreateArray( "CFBundleURLSchemes" );
            array.AddString( "eaz" + eazOwnerID ); //TODO: check if item unique

            rootDict.SetBoolean( "UIViewControllerBasedStatusBarAppearance", false );

            bool EAZEGAMES_LOG_ENABLED = EazeGamesSettingsBase.Instance.GetDebugMode();
            bool EAZEGAMES_USE_SANDBOX_ENVIRONMENT = EazeGamesSettingsBase.Instance.GetEnvironment() == EazeGamesEnvironment.sandbox;

            Debug.Log( "EazeGamesUnityPlugin::Xcode project configured with LogEnabled=" + EAZEGAMES_LOG_ENABLED + " and UseSandboxEnvironment=" + EAZEGAMES_USE_SANDBOX_ENVIRONMENT );

            rootDict.SetBoolean( "EAZEGAMES_LOG_ENABLED", EAZEGAMES_LOG_ENABLED );
            rootDict.SetBoolean( "EAZEGAMES_USE_SANDBOX_ENVIRONMENT", EAZEGAMES_USE_SANDBOX_ENVIRONMENT );

            // Write to file
            File.WriteAllText( plistPath, plist.WriteToString() );
#endif
        }
    }
}
