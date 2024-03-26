namespace Chartboost.Mediation.AmazonPublisherServices
{
    /// <summary>
    /// A request model containing the info to be used by publishers to load an APS ad during pre-bidding.
    ///
    /// Chartboost is not permitted to wrap the Amazon APS initialization or bid request methods directly.
    /// The adapter handles APS initialization and pre-bidding only when the managed pre-bidding flag is enabled.
    /// For more information please contact the Amazon APS support team at https://aps.amazon.com/aps/contact-us/
    /// </summary>
    public struct AmazonPublisherServicesAdapterPreBidRequest
    {
        /// <summary>
        /// Chartboost Mediation's placement identifier.
        /// </summary>
        public readonly string ChartboostPlacement;

        /// <summary>
        /// Ad format.
        /// </summary>
        public readonly string Format;

        /// <summary>
        /// Amazon-specific info needed to load the APS ad.
        /// </summary>
        public readonly AmazonSettings AmazonSettings;
        
        internal AmazonPublisherServicesAdapterPreBidRequest(string chartboostPlacement, string format, AmazonSettings amazonSettings)
        {
            ChartboostPlacement = chartboostPlacement;
            Format = format;
            this.AmazonSettings = amazonSettings;
        }
    }
}
