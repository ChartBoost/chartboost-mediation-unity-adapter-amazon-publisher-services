package com.chartboost.mediation.unity.aps

import com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter
import com.chartboost.mediation.unity.aps.APSPreBidCompletion

interface APSPreBidConsumer {
    fun onPreBid(request: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidRequest, completion: APSPreBidCompletion)
}
