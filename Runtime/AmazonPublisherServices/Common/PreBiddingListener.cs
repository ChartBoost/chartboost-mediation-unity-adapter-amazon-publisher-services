using System.Threading.Tasks;

namespace Chartboost.Mediation.AmazonPublisherServices.Common
{
    public abstract class PreBiddingListener
    {
        public abstract Task<AmazonPublisherServicesAdapterPreBidAdInfo> OnPreBid(AmazonPublisherServicesAdapterPreBidRequest request);
    }
}
