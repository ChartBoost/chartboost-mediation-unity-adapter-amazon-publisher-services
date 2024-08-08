using System;
using Chartboost.Constants;
using Chartboost.Logging;
using Chartboost.Mediation.Ad.Banner;
using Chartboost.Mediation.AmazonPublisherServices.Common;
using Chartboost.Mediation.Android.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Mediation.AmazonPublisherServices.Android
{
    #nullable enable
    internal sealed class AmazonPublisherServicesAdapter : IAmazonPublisherServicesAdapter
    {
        private const string AmazonPublisherServicesAdapterConfiguration = "com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapterConfiguration";
        private const string AmazonPublisherServicesAdapterPreBidAdInfo = "com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter$AmazonPublisherServicesAdapterPreBidAdInfo";
        private const string AmazonPublisherServicesAdapterBridge = "com.chartboost.mediation.unity.adapter.aps.APSBridge";
        private const string AmazonPublisherServicesAdapterPreBidConsumer = "com.chartboost.mediation.unity.adapter.aps.APSPreBidConsumer";
        private const string FunctionSetupPreBiddingListener = "setupPreBiddingListener";
        private const string FunctionRemovePreBiddingListener = "removePreBiddingListener";
        private const string FunctionCompletion = "completion";
        private const string FunctionGetMediationPlacement = "getMediationPlacement";
        private const string FunctionGetFormat = "getFormat";
        private const string FunctionGetKeywords = "getKeywords";
        private const string FunctionGetAmazonSettings = "getAmazonSettings";
        private const string FunctionGetHeight = "getHeight";
        private const string FunctionGetWidth = "getWidth";
        private const string FunctionGetIsVideo = "isVideo";
        private const string FunctionGetPartnerPlacement = "getPartnerPlacement";
        private const string FunctionGetBannerSize = "getBannerSize";
        
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterInstance()
        {
            if (Application.isEditor)
                return;
            AmazonPublisherServices.AmazonPublisherServicesAdapter.Instance = new AmazonPublisherServicesAdapter();
        }
        
        public string AdapterNativeVersion 
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetAdapterVersion);
            }
        }

        public string PartnerSDKVersion
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetPartnerSdkVersion);
            }
        }
        
        public string PartnerIdentifier
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetPartnerId);
            }
        }
        
        public string PartnerDisplayName  
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetPartnerDisplayName);
            }
        }

        public bool TestMode
        {
            get
            {
                using var bridge = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                return bridge.Call<bool>(SharedAndroidConstants.FunctionGetTestMode);
            }
            set
            {
                using var bridge = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                bridge.Call(SharedAndroidConstants.FunctionSetTestMode, value);
            }
        }

        public bool VerboseLogging
        {
            get
            {
                using var bridge = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                return bridge.Call<bool>(SharedAndroidConstants.FunctionGetVerboseLoggingEnabled);
            }
            set
            {
                using var bridge = new AndroidJavaObject(AmazonPublisherServicesAdapterConfiguration);
                bridge.Call(SharedAndroidConstants.FunctionSetVerboseLoggingEnabled, value);
            }
        }

        private static PreBiddingListener? _preBiddingListenerStatic;
        public PreBiddingListener? PreBiddingListener
        {
            get => _preBiddingListenerStatic;
            set
            {
                _preBiddingListenerStatic = value;
                using var bridge = new AndroidJavaClass(AmazonPublisherServicesAdapterBridge);
                if (_preBiddingListenerStatic != null)
                    bridge.CallStatic(FunctionSetupPreBiddingListener, new AmazonPublisherServicesPreBidConsumer());
                else
                    bridge.CallStatic(FunctionRemovePreBiddingListener);
            }
        }

        internal class AmazonPublisherServicesPreBidConsumer : AndroidJavaProxy
        {
            public AmazonPublisherServicesPreBidConsumer() : base(AmazonPublisherServicesAdapterPreBidConsumer) { }

            [Preserve]
            // ReSharper disable once InconsistentNaming
            public void onPreBid(AndroidJavaObject request, AndroidJavaObject completion)
            {
                if (_preBiddingListenerStatic == null)
                    return;

                MainThreadDispatcher.MainThreadTask(async () =>
                {
                    try
                    {
                        var mediationPlacement = request.Call<string>(FunctionGetMediationPlacement);
                        var adFormat = request.Call<AndroidJavaObject>(FunctionGetFormat).Call<string>(SharedAndroidConstants.FunctionToString).ToLower();
                        var keywords = request.Call<AndroidJavaObject>(FunctionGetKeywords).MapToDictionary();
                        var bannerSizeNative = request.Call<AndroidJavaObject>(FunctionGetBannerSize);

                        BannerSize? bannerSize = null;
                        if (bannerSizeNative != null)
                            bannerSize = bannerSizeNative.ToBannerSize();

                        var amazonSettings = request.Call<AndroidJavaObject>(FunctionGetAmazonSettings);
                        var height = amazonSettings.Call<int>(FunctionGetHeight);
                        var width = amazonSettings.Call<int>(FunctionGetWidth);
                        var isVideo = amazonSettings.Call<bool>(FunctionGetIsVideo);
                        var partnerPlacement = amazonSettings.Call<string>(FunctionGetPartnerPlacement);
                        
                        var unityAmazonSettings = new  AmazonSettings(partnerPlacement, isVideo, height, width);
                        
                        var unityPreBidRequest = new AmazonPublisherServicesAdapterPreBidRequest(mediationPlacement, adFormat, keywords, bannerSize, unityAmazonSettings);
                        AmazonPublisherServicesAdapterPreBidAdInfo? unityPreBidAdInfo = await _preBiddingListenerStatic.OnPreBid(unityPreBidRequest);
                        using var nativePreBidAdInfo = new AndroidJavaObject(AmazonPublisherServicesAdapterPreBidAdInfo, unityPreBidAdInfo?.PricePoint, unityPreBidAdInfo?.BidInfo);
                        completion.Call(FunctionCompletion, nativePreBidAdInfo);
                    }
                    catch (Exception exception)
                    {
                        LogController.LogException(exception);
                    }
                });
            }
        }
    }
}
