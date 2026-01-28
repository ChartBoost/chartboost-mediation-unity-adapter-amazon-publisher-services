using Chartboost.Mediation.Adapters;
using Chartboost.Mediation.AmazonPublisherServices.Common;
using Chartboost.Mediation.AmazonPublisherServices.Default;

namespace Chartboost.Mediation.AmazonPublisherServices
{
    #nullable enable
    /// <inheritdoc cref="IAmazonPublisherServicesAdapter"/>
    public static class AmazonPublisherServicesAdapter 
    {
        internal static IAmazonPublisherServicesAdapter Instance = new AmazonPublisherServicesDefault();

        /// <summary>
        /// The partner adapter Unity version.
        /// </summary>
        public const string AdapterUnityVersion = "5.2.1";

        /// <inheritdoc cref="IPartnerAdapterConfiguration.AdapterNativeVersion"/>
        public static string AdapterNativeVersion => Instance.AdapterNativeVersion;
        
        /// <inheritdoc cref="IPartnerAdapterConfiguration.PartnerSDKVersion"/>
        public static string PartnerSDKVersion => Instance.PartnerSDKVersion;
        
        /// <inheritdoc cref="IPartnerAdapterConfiguration.PartnerIdentifier"/>
        public static string PartnerIdentifier => Instance.PartnerIdentifier;
        
        /// <inheritdoc cref="IPartnerAdapterConfiguration.PartnerDisplayName"/>
        public static string PartnerDisplayName => Instance.PartnerDisplayName;

        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.TestMode"/>
        public static bool TestMode { get => Instance.TestMode; set => Instance.TestMode = value; }

        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.VerboseLogging"/>
        public static bool VerboseLogging { get => Instance.VerboseLogging; set => Instance.VerboseLogging = value; }
        
        /// <inheritdoc cref="IAmazonPublisherServicesAdapter.PreBiddingListener"/>
        public static PreBiddingListener? PreBiddingListener { get => Instance.PreBiddingListener; set => Instance.PreBiddingListener = value; }
    }
}
