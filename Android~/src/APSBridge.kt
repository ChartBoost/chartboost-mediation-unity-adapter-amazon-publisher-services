package com.chartboost.mediation.unity.adapter.aps

import android.content.Context
import com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

class APSBridge {

    companion object {

        private var preBiddingListener: AmazonPublisherServicesAdapter.PreBiddingListener? = null

        @JvmStatic
        fun setupPreBiddingListener(consumer: APSPreBidConsumer){
            preBiddingListener = object : AmazonPublisherServicesAdapter.PreBiddingListener {
                override suspend fun onPreBid(context: Context, request: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidRequest): Result<AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidAdInfo> {
                    return suspendCoroutine { continuation ->
                        consumer.onPreBid(request,  object: APSPreBidCompletion {
                            override fun completion(preBidAdInfo: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidAdInfo) {
                                continuation.resume(Result.success(preBidAdInfo))
                            }
                        })
                    }
                }
            }
            AmazonPublisherServicesAdapter.preBiddingListener = preBiddingListener;
        }

        @JvmStatic
        fun removePreBiddingListener()
        {
            preBiddingListener = null;
            AmazonPublisherServicesAdapter.preBiddingListener = null;
        }
    }
}
