#ifndef Utils_h
#define Utils_h

#import <Foundation/Foundation.h>

@interface EAZUnityUtils : NSObject
+(void) setEventObjectName: (const char*) name;
+(void) sendUnityEvent:(const char*) event data: (const char*) data;
+(void) sendLog: (const NSString*) message;
+(void) log: (const NSString*) message force: (BOOL) force;
+(void) enableLog: (BOOL) arg;
@end

#endif /* Utils_h */
