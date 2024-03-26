using System.Threading.Tasks;
using Chartboost.Mediation.AmazonPublisherServices.Common;

namespace Chartboost.Mediation.AmazonPublisherServices.Default
{
    /// <inheritdoc cref="IAmazonPublisherServicesAdapter"/>
    internal sealed class AmazonPublisherServicesDefault : IAmazonPublisherServicesAdapter
    {
        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.TestMode"/>
        public bool TestMode { get; set; }
        
        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.VerboseLogging"/>
        public bool VerboseLogging { get; set; }

        public PreBiddingListener PreBiddingListener { get; set; } = new DefaultPreBiddingListener();

        /// <summary>
        /// Example pre-bidding listener
        /// </summary>
        private class DefaultPreBiddingListener : PreBiddingListener
        {
            public override Task<AmazonPublisherServicesAdapterPreBidAdInfo> OnPreBid(AmazonPublisherServicesAdapterPreBidRequest request)
            {
                return Task.FromResult(new AmazonPublisherServicesAdapterPreBidAdInfo(null, null));
            }
        }
    }
}
