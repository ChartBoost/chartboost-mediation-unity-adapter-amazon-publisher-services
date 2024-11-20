package com.chartboost.mediation.unity.adapter.aps

import android.content.Context
import com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter
import com.chartboost.mediation.unity.logging.LogLevel
import com.chartboost.mediation.unity.logging.UnityLoggingBridge
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

@Suppress("unused")
class APSBridge {

    companion object {

        private val TAG = APSBridge::class.simpleName
        private var preBiddingListener: AmazonPublisherServicesAdapter.PreBiddingListener? = null

        @JvmStatic
        fun setupPreBiddingListener(consumer: APSPreBidConsumer){
            preBiddingListener = object : AmazonPublisherServicesAdapter.PreBiddingListener {
                override suspend fun onPreBid(context: Context, request: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidRequest): Result<AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidAdInfo> {
                    UnityLoggingBridge.log(TAG, "PreBid request has been obtained for mediation placement: ${request.mediationPlacement}", LogLevel.VERBOSE)
                    return suspendCoroutine { continuation ->
                        consumer.onPreBid(request,  object: APSPreBidCompletion {
                            override fun completion(preBidAdInfo: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidAdInfo) {
                                UnityLoggingBridge.log(TAG, "PreBid request has been completed for mediation placement: ${request.mediationPlacement}", LogLevel.VERBOSE)
                                continuation.resume(Result.success(preBidAdInfo))
                            }
                        })
                    }
                }
            }
            AmazonPublisherServicesAdapter.preBiddingListener = preBiddingListener
            UnityLoggingBridge.log(TAG, "PreBiddingListener successfully set", LogLevel.VERBOSE)
        }

        @JvmStatic
        fun removePreBiddingListener()
        {
            preBiddingListener = null;
            AmazonPublisherServicesAdapter.preBiddingListener = null;
            UnityLoggingBridge.log(TAG, "PreBiddingListener removed", LogLevel.VERBOSE)
        }
    }
}
