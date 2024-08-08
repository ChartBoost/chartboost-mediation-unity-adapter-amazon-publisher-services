package com.chartboost.mediation.unity.adapter.aps

import com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter
interface APSPreBidConsumer {
    fun onPreBid(request: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidRequest, completion: APSPreBidCompletion)
}
