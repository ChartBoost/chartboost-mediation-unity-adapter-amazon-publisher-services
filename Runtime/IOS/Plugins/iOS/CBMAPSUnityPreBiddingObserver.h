#import <Foundation/Foundation.h>
#import "ChartboostUnityUtilities.h"
#import <ChartboostMediationAdapterAmazonPublisherServices/ChartboostMediationAdapterAmazonPublisherServices-Swift.h>

typedef void (*ChartboostMediationAPSUnityPreBidConsumer)(const char* _Nonnull chartboostPlacement, const char* _Nonnull adFormat, int height, int width, bool isVideo, const char* _Nullable partnerPlacement);

@interface CBAPSUnityPreBiddingObserver : NSObject<AmazonPublisherServicesAdapterPreBiddingDelegate>

+ (instancetype _Nonnull) sharedObserver;
- (void) setupPreBiddingListener:(ChartboostMediationAPSUnityPreBidConsumer _Nonnull)onPreBid;
- (void) removePreBiddingListener;
- (void) completePreBid:(AmazonPublisherServicesAdapterPreBidResult*_Nonnull)preBidResult;

@property _Nullable ChartboostMediationAPSUnityPreBidConsumer preBidConsumer;
@property (nonnull, nonatomic) void (^preBidCompleter)(AmazonPublisherServicesAdapterPreBidResult * _Nonnull);

@end
