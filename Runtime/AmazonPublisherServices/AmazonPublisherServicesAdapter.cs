using Chartboost.Mediation.AmazonPublisherServices.Common;
using Chartboost.Mediation.AmazonPublisherServices.Default;

namespace Chartboost.Mediation.AmazonPublisherServices
{
    #nullable enable
    /// <inheritdoc cref="IAmazonPublisherServicesAdapter"/>
    public sealed class AmazonPublisherServicesAdapter
    {
        /// <summary>
        /// Adapter's Unity version.
        /// </summary>
        public static string Version => "4.0.0";
        
        internal static IAmazonPublisherServicesAdapter Instance = new AmazonPublisherServicesDefault();

        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.TestMode"/>
        public static bool TestMode { get => Instance.TestMode; set => Instance.TestMode = value; }

        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.VerboseLogging"/>
        public static bool VerboseLogging { get => Instance.VerboseLogging; set => Instance.VerboseLogging = value; }
        
        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.PreBiddingListener"/>
        public static PreBiddingListener? PreBiddingListener { get => Instance.PreBiddingListener; set => Instance.PreBiddingListener = value; }
    }
    #nullable disable
}
