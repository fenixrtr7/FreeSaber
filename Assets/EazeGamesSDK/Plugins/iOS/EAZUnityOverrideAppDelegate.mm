#import "UnityAppController.h"
#import "EAZUnityManager.h"
#import "EAZUnityUtils.h"
@interface EAZUnityOverrideAppDelegate : UnityAppController
@end

IMPL_APP_CONTROLLER_SUBCLASS(EAZUnityOverrideAppDelegate)

@implementation EAZUnityOverrideAppDelegate

- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{
    bool logEnabled = [[[NSBundle mainBundle] objectForInfoDictionaryKey:@"EAZEGAMES_LOG_ENABLED"] boolValue];
    bool useSandbox = [[[NSBundle mainBundle] objectForInfoDictionaryKey:@"EAZEGAMES_USE_SANDBOX_ENVIRONMENT"] boolValue];
    [EAZUnityUtils log: [NSString stringWithFormat:@"SDK configured with LOG_ENABLED=%s and USE_SANDBOX=%s",(logEnabled? "YES" : "NO"), (useSandbox? "YES" : "NO")] force: true ];
    [EAZUnityUtils enableLog:logEnabled];
    [EAZUnityManager setEnvironmentBool: useSandbox ];
    //[EazManager setPreparationDelegate ];
    return [super application:application didFinishLaunchingWithOptions:launchOptions];
}

- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options/* NS_AVAILABLE_IOS(9_0)*/
{
    [EAZUnityManager handleDeepLinkURL: url];
    return true;
    //check if it is not hides super method logic
    //return [super application:app openURL:url options: options];
}

- (void)application:(UIApplication*)application didReceiveRemoteNotification:(NSDictionary*)userInfo
{
    [EAZUnityManager handlePushNotificationInfo :userInfo ];
#if UNITY_USES_REMOTE_NOTIFICATIONS
    [super application:application didReceiveRemoteNotification:userInfo];
#endif
}

#if !PLATFORM_TVOS

- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult result))handler
{
    [EAZUnityManager handlePushNotificationInfo :userInfo ];
#if UNITY_USES_REMOTE_NOTIFICATIONS
    [super application:application didReceiveRemoteNotification:userInfo fetchCompletionHandler:handler];
#else
    if (handler)
    {
        handler(UIBackgroundFetchResultNoData);
    }
#endif
}

#endif

@end

