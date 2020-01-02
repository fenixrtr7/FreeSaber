#ifndef EAZUnityManager_h
#define EAZUnityManager_h

#import <Foundation/Foundation.h>

//! Project version number for EAZUnityManager.
FOUNDATION_EXPORT double EAZUnityManagerVersionNumber;

//! Project version string for EAZUnityManager.
FOUNDATION_EXPORT const unsigned char EAZUnityManagerVersionString[];

@interface EAZUnityManager: NSObject

+(void) setEnvironment:(const char*) param;
+(void) setEnvironmentBool:(BOOL) is_sandbox;
+(void) setPreparationDelegate;
+(void) gameDidLoad;
+(void) didStartPlaying;
+(void) finishPlayingWithFinalScore: (NSInteger) score;
+(void) sendScore: (NSInteger) score;
+(void) leaveGame;
+(void) setFCMToken:(const char*) token;
+(void) handleDeepLink:(const char*) surl;
+(void) handleDeepLinkURL:(NSURL *) url;
+(void) handlePushNotificationInfo : (NSDictionary*)userInfo;
@end

#endif /* EAZUnityManager_h */
