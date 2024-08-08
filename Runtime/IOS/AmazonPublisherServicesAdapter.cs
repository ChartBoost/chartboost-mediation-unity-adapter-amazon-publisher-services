using System;
using System.Runtime.InteropServices;
using AOT;
using Chartboost.Constants;
using Chartboost.Logging;
using Chartboost.Mediation.AmazonPublisherServices.Common;
using Chartboost.Mediation.Utilities;
using UnityEngine;

namespace Chartboost.Mediation.AmazonPublisherServices.IOS
{
    #nullable enable
    internal sealed class AmazonPublisherServicesAdapter : IAmazonPublisherServicesAdapter
    {
        private delegate void ChartboostMediationAmazonPublisherServicesPreBidConsumer(string mediationPlacement, string adFormat, string keywordsJson, string bannerSizeJson, int height, int width, bool isVideo, string partnerPlacement);
        
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterInstance()
        {
            if (Application.isEditor)
                return;
            AmazonPublisherServices.AmazonPublisherServicesAdapter.Instance = new AmazonPublisherServicesAdapter();
        }

        /// <inheritdoc/>
        public string AdapterNativeVersion => _CBMAmazonPublisherServicesAdapterAdapterVersion();
        
        /// <inheritdoc/>
        public string PartnerSDKVersion => _CBMAmazonPublisherServicesAdapterPartnerSDKVersion();
        
        /// <inheritdoc/>
        public string PartnerIdentifier => _CBMAmazonPublisherServicesAdapterPartnerId();
        
        /// <inheritdoc/>
        public string PartnerDisplayName => _CBMAmazonPublisherServicesAdapterPartnerDisplayName();
        
        /// <inheritdoc/>
        public bool TestMode
        {
            get => _CBMAmazonPublisherServicesAdapterGetTestMode();
            set => _CBMAmazonPublisherServicesAdapterSetTestMode(value);
        }
        
        /// <inheritdoc/>
        public bool VerboseLogging
        {
            get => _CBMAmazonPublisherServicesAdapterGetVerboseLogging();
            set => _CBMAmazonPublisherServicesAdapterSetVerboseLogging(value);
        }
        
        private static PreBiddingListener? _preBiddingListenerStatic;
        
        /// <inheritdoc/>
        public PreBiddingListener? PreBiddingListener
        {
            get => _preBiddingListenerStatic;
            set
            {
                _preBiddingListenerStatic = value;
                if (_preBiddingListenerStatic != null)
                    _CBMAmazonPublisherServicesSetupPreBiddingListener(OnPreBid);
                else
                    _CBMAmazonPublisherServicesRemovePreBiddingListener();
            }
        }
        
        [MonoPInvokeCallback(typeof(ChartboostMediationAmazonPublisherServicesPreBidConsumer))]
        private static void OnPreBid(string mediationPlacement, string adFormat, string keywordsJson, string bannerSizeJson, int height, int width, bool isVideo, string partnerPlacement)
        {
            if (_preBiddingListenerStatic == null)
                return;
            
            MainThreadDispatcher.MainThreadTask(async () =>
            {
                AmazonPublisherServicesAdapterPreBidAdInfo? unityPreBidAdInfo = null;
                try
                {
                    var unityAmazonSettings = new AmazonSettings(partnerPlacement, isVideo, height, width);
                    var unityPreBidRequest = new AmazonPublisherServicesAdapterPreBidRequest(mediationPlacement, adFormat, keywordsJson.ToDictionary(), bannerSizeJson.ToBannerSize(), unityAmazonSettings);
                    unityPreBidAdInfo = await _preBiddingListenerStatic.OnPreBid(unityPreBidRequest);
                }
                catch (Exception exception)
                { 
                    LogController.LogException(exception);
                }
                finally
                {
                    _CBMAmazonPublisherServicesPreBidCompletion(unityPreBidAdInfo?.PricePoint, unityPreBidAdInfo?.BidInfo);
                }
            });
        }
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMAmazonPublisherServicesAdapterAdapterVersion();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMAmazonPublisherServicesAdapterPartnerSDKVersion();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMAmazonPublisherServicesAdapterPartnerId();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMAmazonPublisherServicesAdapterPartnerDisplayName();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBMAmazonPublisherServicesAdapterGetTestMode();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMAmazonPublisherServicesAdapterSetTestMode(bool testMode); 
        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBMAmazonPublisherServicesAdapterGetVerboseLogging();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMAmazonPublisherServicesAdapterSetVerboseLogging(bool testMode);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMAmazonPublisherServicesSetupPreBiddingListener(ChartboostMediationAmazonPublisherServicesPreBidConsumer preBidConsumer);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMAmazonPublisherServicesRemovePreBiddingListener();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMAmazonPublisherServicesPreBidCompletion(string? pricePoint, string? bidInfo);
    }
}
