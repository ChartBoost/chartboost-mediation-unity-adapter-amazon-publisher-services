namespace Chartboost.Mediation.AmazonPublisherServices.Common
{
    #nullable enable
    /// <summary>
    /// The Chartboost Mediation Amazon Publisher Services (APS) adapter.
    /// </summary>
    public interface IAmazonPublisherServicesAdapter
    {
        /// <summary>
        /// Flag that can optionally be set to enable the partner's test mode.
        /// Disabled by default.
        /// </summary>
        public bool TestMode { get; set; }

        /// <summary>
        /// Flag that can optionally be set to enable the partner's verbose logging.
        /// Disabled by default.
        /// </summary>
        public bool VerboseLogging { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PreBiddingListener? PreBiddingListener { get; set; }
    }
    #nullable disable
}
