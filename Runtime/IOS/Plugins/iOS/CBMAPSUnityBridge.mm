#import "CBMAPSUnityPreBiddingObserver.h"

extern "C" {
    bool _CBMAmazonPublisherServicesAdapterGetTestMode() {
        return [AmazonPublisherServicesAdapterConfiguration testMode];
    }

    void _CBMAmazonPublisherServicesAdapterSetTestMode(bool testMode) {
        [AmazonPublisherServicesAdapterConfiguration setTestMode:testMode];
    }

    bool _CBMAmazonPublisherServicesAdapterGetVerboseLogging() {
        return [AmazonPublisherServicesAdapterConfiguration verboseLogging];
    }

    void _CBMAmazonPublisherServicesAdapterSetVerboseLogging(bool verboseLogging) {
        [AmazonPublisherServicesAdapterConfiguration setVerboseLogging:verboseLogging];
    }

    void _CBMAmazonPublisherServicesSetupPreBiddingListener(ChartboostMediationAPSUnityPreBidConsumer onPreBid){
        [[CBAPSUnityPreBiddingObserver sharedObserver] setupPreBiddingListener:onPreBid];
    }

    void _CBMAmazonPublisherServicesRemovePreBiddingListener(){
        [[CBAPSUnityPreBiddingObserver sharedObserver] removePreBiddingListener];
    }

    void _CBMAmazonPublisherServicesPreBidCompletion(const char* pricePoint, const char* bidInfo)
    {
        AmazonPublisherServicesAdapterPreBidAdInfo* adInfo = [[AmazonPublisherServicesAdapterPreBidAdInfo alloc] initWithPricePoint:toNSStringOrEmpty(pricePoint) bidPayload:toNSDictionary(bidInfo)];
        AmazonPublisherServicesAdapterPreBidResult* preBidResult = [[AmazonPublisherServicesAdapterPreBidResult alloc] initWithAdInfo:adInfo];
        [[CBAPSUnityPreBiddingObserver sharedObserver] completePreBid:preBidResult];
    }
}
