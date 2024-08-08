namespace Chartboost.Mediation.AmazonPublisherServices
{
    #nullable enable
    /// <summary>
    /// APS Ad info obtained during pre-bidding.
    /// </summary>
    public struct AmazonPublisherServicesAdapterPreBidAdInfo
    {
        /// <summary>
        /// The associated price point.
        /// </summary>
        public readonly string? PricePoint;

        /// <summary>
        /// The associated bid payload.
        /// </summary>
        public readonly string? BidInfo;

        public AmazonPublisherServicesAdapterPreBidAdInfo(string? pricePoint, string? bidInfo)
        {
            PricePoint = pricePoint;
            BidInfo = bidInfo;
        }
    }
}
