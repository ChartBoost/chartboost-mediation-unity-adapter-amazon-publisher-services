using System;
using System.Runtime.InteropServices;
using AOT;
using Chartboost.Mediation.AmazonPublisherServices.Common;
using UnityEngine;

namespace Chartboost.Mediation.AmazonPublisherServices.IOS
{
    #nullable enable
    public class AmazonPublisherServicesAdapter : IAmazonPublisherServicesAdapter
    {
        private delegate void ChartboostMediationAmazonPublisherServicesPreBidConsumer(string chartboostPlacement, string adFormat, int height, int width, bool isVideo, string partnerPlacement);
        
        private const string Internal = "__Internal";
        
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterInstance()
        {
            if (Application.isEditor)
                return;
            AmazonPublisherServices.AmazonPublisherServicesAdapter.Instance = new AmazonPublisherServicesAdapter();
        }
        
        public bool TestMode
        {
            get => _CBMAmazonPublisherServicesAdapterGetTestMode();
            set => _CBMAmazonPublisherServicesAdapterSetTestMode(value);
        }
        
        public bool VerboseLogging
        {
            get => _CBMAmazonPublisherServicesAdapterGetVerboseLogging();
            set => _CBMAmazonPublisherServicesAdapterSetVerboseLogging(value);
        }
        
        private static PreBiddingListener? _preBiddingListenerStatic;
        
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
        private static void OnPreBid(string chartboostPlacement, string adFormat, int height, int width, bool isVideo, string partnerPlacement)
        {
            if (_preBiddingListenerStatic == null)
                return;
            
            MainThreadDispatcher.MainThreadTask(async () =>
            {
                AmazonPublisherServicesAdapterPreBidAdInfo? unityPreBidAdInfo = null;
                try
                {
                    var unityAmazonSettings = new AmazonSettings(partnerPlacement, isVideo, height, width);
                    var unityPreBidRequest = new AmazonPublisherServicesAdapterPreBidRequest(chartboostPlacement, adFormat, unityAmazonSettings);
                    unityPreBidAdInfo = await _preBiddingListenerStatic.OnPreBid(unityPreBidRequest);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                finally
                {
                    _CBMAmazonPublisherServicesPreBidCompletion(unityPreBidAdInfo?.PricePoint, unityPreBidAdInfo?.BidInfo);
                }
            });
        }

        [DllImport(Internal)] private static extern void _CBMAmazonPublisherServicesSetupPreBiddingListener(ChartboostMediationAmazonPublisherServicesPreBidConsumer preBidConsumer);
        [DllImport(Internal)] private static extern void _CBMAmazonPublisherServicesRemovePreBiddingListener();
        [DllImport(Internal)] private static extern void _CBMAmazonPublisherServicesPreBidCompletion(string? pricePoint, string? bidInfo);
        [DllImport(Internal)] private static extern bool _CBMAmazonPublisherServicesAdapterGetTestMode();
        [DllImport(Internal)] private static extern void _CBMAmazonPublisherServicesAdapterSetTestMode(bool testMode); 
        [DllImport(Internal)] private static extern bool _CBMAmazonPublisherServicesAdapterGetVerboseLogging();
        [DllImport(Internal)] private static extern void _CBMAmazonPublisherServicesAdapterSetVerboseLogging(bool testMode);
    }
    #nullable disable
}
