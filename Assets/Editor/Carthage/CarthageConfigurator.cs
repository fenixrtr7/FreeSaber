using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IPHONE
using UnityEditor.iOS.Extensions;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EazeGamesSDK
{
    public class CarthageConfigurator
    {
#if UNITY_IPHONE

        [PostProcessBuild]
        static void Configure(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget != BuildTarget.iOS)
                return;

            RunCarthageUpdate(buildPath);
            AddCarthageFrameworks(buildPath);
            //AddCopyFrameworksBuildPhase(buildPath); //this step is not working yet, will investigte it later
        }

        static void RunCarthageUpdate(string buildPath)
        {
            var files = new List<string>
        {
            "Cartfile"
        };

            foreach (var x in files)
            {
                File.Copy(CombinePaths(Application.dataPath, "Editor", "Carthage", x), CombinePaths(buildPath, x), true);
            }

            var command = new ProcessStartInfo
            {
                WorkingDirectory = buildPath,
                FileName = "/usr/local/bin/carthage",
                Arguments = "update --platform iOS",
                CreateNoWindow = true
            };

            Process process = Process.Start(command);
            process.WaitForExit();
            process.Close();
        }

        static void AddCarthageFrameworks(string buildPath)
        {
            var project = new PBXProject();
            var path = PBXProject.GetPBXProjectPath(buildPath);

            project.ReadFromFile(path);

            var target = project.TargetGuidByName(PBXProject.GetUnityTargetName());

            var frameworks = Directory.GetDirectories(CombinePaths(buildPath, "Carthage", "Build", "iOS"), "*.framework");
            foreach (var x in frameworks)
            {
                var name = Path.GetFileName(x);
                string fileGuid = project.AddFile("Carthage/Build/iOS/" + name, "Frameworks/" + name, PBXSourceTree.Source);
                project.AddFileToBuild(target, fileGuid);
                //PBXProjectExtensions.AddFileToEmbedFrameworks(project, target, fileGuid);
            }

            project.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Carthage/Build/iOS");
            project.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");

            project.WriteToFile(path);
        }

        static void AddCopyFrameworksBuildPhase(string buildPath)
        {
            var files = new List<string>
        {
            "Gemfile",
            "carthage_copy_frameworks.rb"
        };

            foreach (var x in files)
            {
                File.Copy(CombinePaths(Application.dataPath, "Editor", "Carthage", x), CombinePaths(buildPath, x), true);
            }

            var commands = new List<ProcessStartInfo>
        {
                //following steps should be done once on working machine
            /*new ProcessStartInfo
            {
                Arguments = "gem install bundler --no-ri --no-rdoc"
            },*/
            new ProcessStartInfo
            {
                Arguments = "bundle install --path vendor/bundle"
            },
            new ProcessStartInfo
            {
                //Arguments = "bundle exec ruby carthage_copy_frameworks.rb"
                Arguments = "ruby carthage_copy_frameworks.rb"
            }
        };

            foreach (var x in commands)
            {
                x.EnvironmentVariables["PATH"] = string.Join(":", new string[]
                    {
                    x.EnvironmentVariables["HOME"] + "/.rbenv/shims",
                    x.EnvironmentVariables["PATH"]
                    });

                x.UseShellExecute = false;

                x.FileName = "/bin/sh";
                x.WorkingDirectory = buildPath;

                x.CreateNoWindow = true;

                var process = Process.Start(x);
                process.WaitForExit();

                process.Close();
            }
        }

        private static string CombinePaths(params string[] paths)
        {
            return paths.Aggregate(Path.Combine);
        }
#endif

    }
}