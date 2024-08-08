using System.Threading.Tasks;
using Chartboost.Json;
using Chartboost.Logging;
using Chartboost.Mediation.AmazonPublisherServices.Common;

namespace Chartboost.Mediation.AmazonPublisherServices.Default
{
    /// <inheritdoc cref="IAmazonPublisherServicesAdapter"/>
    internal sealed class AmazonPublisherServicesDefault : IAmazonPublisherServicesAdapter
    {
        /// <inheritdoc/>
        public string AdapterNativeVersion => AmazonPublisherServicesAdapter.AdapterUnityVersion;

        /// <inheritdoc/>
        public string PartnerSDKVersion => AmazonPublisherServicesAdapter.AdapterUnityVersion;
        
        /// <inheritdoc/>
        public string PartnerIdentifier => "amazon_aps";
        
        /// <inheritdoc/>
        public string PartnerDisplayName => "Amazon Publisher Services";
        
        /// <inheritdoc/>
        public bool TestMode { get; set; }
        
        /// <inheritdoc/>
        public bool VerboseLogging { get; set; }

        public PreBiddingListener PreBiddingListener { get; set; } = new DefaultPreBiddingListener();

        /// <summary>
        /// Example pre-bidding listener
        /// </summary>
        private class DefaultPreBiddingListener : PreBiddingListener
        {
            public override Task<AmazonPublisherServicesAdapterPreBidAdInfo> OnPreBid(AmazonPublisherServicesAdapterPreBidRequest request)
            {
                LogController.Log($"Receiving PreBid request with the following contents: {JsonTools.SerializeObject(request)}", LogLevel.Debug);
                return Task.FromResult(new AmazonPublisherServicesAdapterPreBidAdInfo(null, null));
            }
        }
    }
}
