using System;
using Chartboost.Mediation.AmazonPublisherServices.Common;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Mediation.AmazonPublisherServices.Android
{
    #nullable enable
    internal class AmazonPublisherServicesAdapter : IAmazonPublisherServicesAdapter
    {
        private const string ClassAmazonPublisherServicesAdapterPreBidConsumer = "com.chartboost.mediation.unity.aps.APSPreBidConsumer";
        private const string ClassAmazonPublisherServicesAdapterBridge = "com.chartboost.mediation.unity.aps.APSBridge";
        private const string ClassAmazonPublisherServicesAdapterPreBidAdInfo = "com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter$AmazonPublisherServicesAdapterPreBidAdInfo";
        private const string FunSetupPreBiddingListener = "setupPreBiddingListener";
        private const string FunRemovePreBiddingListener = "removePreBiddingListener";
        private const string FunCompletion = "completion";
        private const string FunToString = "toString";
        private const string GetTestMode = "getTestMode";
        private const string SetTestMode = "setTestMode";
        private const string FunGetChartboostPlacement = "getChartboostPlacement";
        private const string FunGetFormat = "getFormat";
        private const string FunGetAmazonSettings = "getAmazonSettings";
        private const string FunGetHeight = "getHeight";
        private const string FunGetWidth = "getWidth";
        private const string FunGetIsVideo = "isVideo";
        private const string FunGetPartnerPlacement = "getPartnerPlacement";
        
        private AndroidJavaProxy? _consumerInstance;
        
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterInstance()
        {
            if (Application.isEditor)
                return;
            AmazonPublisherServices.AmazonPublisherServicesAdapter.Instance = new AmazonPublisherServicesAdapter();
        }

        public bool TestMode
        {
            get
            {
                using var bridge = new AndroidJavaClass(ClassAmazonPublisherServicesAdapterBridge);
                return bridge.CallStatic<bool>(GetTestMode);
            }
            set
            {
                using var bridge = new AndroidJavaClass(ClassAmazonPublisherServicesAdapterBridge);
                bridge.CallStatic(SetTestMode, value);
            }
        }

        public bool VerboseLogging { get; set; } // Do nothing, maybe we can introduce adapter bridge debugging in the future?

        private static PreBiddingListener? _preBiddingListenerStatic;
        public PreBiddingListener? PreBiddingListener
        {
            get => _preBiddingListenerStatic;
            set
            {
                _preBiddingListenerStatic = value;
                using var bridge = new AndroidJavaClass(ClassAmazonPublisherServicesAdapterBridge);
                if (_preBiddingListenerStatic != null)
                {
                    _consumerInstance ??= new AmazonPublisherServicesPreBidConsumer();
                    bridge.CallStatic(FunSetupPreBiddingListener, _consumerInstance);
                }
                else
                    bridge.CallStatic(FunRemovePreBiddingListener);
            }
        }

        internal class AmazonPublisherServicesPreBidConsumer : AndroidJavaProxy
        {
            public AmazonPublisherServicesPreBidConsumer() : base(ClassAmazonPublisherServicesAdapterPreBidConsumer) { }

            [Preserve]
            public void onPreBid(AndroidJavaObject request, AndroidJavaObject completion)
            {
                if (_preBiddingListenerStatic == null)
                    return;

                MainThreadDispatcher.MainThreadTask(async () =>
                {
                    try
                    {
                        var chartboostPlacement = request.Call<string>(FunGetChartboostPlacement);
                        var adFormat = request.Call<AndroidJavaObject>(FunGetFormat).Call<string>(FunToString).ToLower();

                        var amazonSettings = request.Call<AndroidJavaObject>(FunGetAmazonSettings);
                        var height = amazonSettings.Call<int>(FunGetHeight);
                        var width = amazonSettings.Call<int>(FunGetWidth);
                        var isVideo = amazonSettings.Call<bool>(FunGetIsVideo);
                        var partnerPlacement = amazonSettings.Call<string>(FunGetPartnerPlacement);
                        var unityAmazonSettings = new  AmazonSettings(partnerPlacement, isVideo, height, width);
                        
                        var unityPreBidRequest = new AmazonPublisherServicesAdapterPreBidRequest(chartboostPlacement, adFormat, unityAmazonSettings);
                        AmazonPublisherServicesAdapterPreBidAdInfo? unityPreBidAdInfo = await _preBiddingListenerStatic.OnPreBid(unityPreBidRequest);
                        using var nativePreBidAdInfo = new AndroidJavaObject(ClassAmazonPublisherServicesAdapterPreBidAdInfo, unityPreBidAdInfo?.PricePoint, unityPreBidAdInfo?.BidInfo);
                        completion.Call(FunCompletion, nativePreBidAdInfo);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                });
            }
        }
    }
    #nullable disable
}
