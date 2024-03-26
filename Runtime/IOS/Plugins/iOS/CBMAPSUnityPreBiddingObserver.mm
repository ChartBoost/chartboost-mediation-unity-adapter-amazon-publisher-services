#import "CBMAPSUnityPreBiddingObserver.h"

@implementation CBAPSUnityPreBiddingObserver

+ (instancetype) sharedObserver {
    static dispatch_once_t pred = 0;
    static id _sharedObject = nil;
    dispatch_once(&pred, ^ {
        _sharedObject = [[self alloc] init];
    });

    return _sharedObject;
}

- (void)setupPreBiddingListener:(ChartboostMediationAPSUnityPreBidConsumer)onPreBid  {
    _preBidConsumer = onPreBid;
    [AmazonPublisherServicesAdapterConfiguration setPreBiddingDelegate:self];
}

- (void)removePreBiddingListener {
    _preBidConsumer = nil;
    [AmazonPublisherServicesAdapterConfiguration setPreBiddingDelegate:nil];
}

- (void)onPreBidWithRequest:(AmazonPublisherServicesAdapterPreBidRequest * _Nonnull)request completion:(void (^ _Nonnull)(AmazonPublisherServicesAdapterPreBidResult * _Nonnull))completion {
    if (_preBidConsumer == nil)
        return;

    _preBidCompleter = ^(AmazonPublisherServicesAdapterPreBidResult* preBidResult){
        completion(preBidResult);
    };

    const char* chartboostPlacement = [[request chartboostPlacement] UTF8String];
    const char* format = [[request format] UTF8String];
    int height = [request amazonSettings] ? (int)[[request amazonSettings] height] : 0;
    int width = [request amazonSettings] ? (int)[[request amazonSettings] width] : 0;
    bool isVideo = [request amazonSettings] ? [[request amazonSettings] video] : false;
    const char * partnerPlacement = [request amazonSettings] ? [[[request amazonSettings] partnerPlacement] UTF8String] : nil;

    _preBidConsumer(chartboostPlacement, format, height, width, isVideo, partnerPlacement);
}

- (void)completePreBid:(AmazonPublisherServicesAdapterPreBidResult*)preBidResult {
    if (_preBidCompleter != nil)
        _preBidCompleter(preBidResult);
}

@end
