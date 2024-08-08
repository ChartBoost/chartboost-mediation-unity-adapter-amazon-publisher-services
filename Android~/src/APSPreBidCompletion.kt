package com.chartboost.mediation.unity.adapter.aps

import com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter
interface APSPreBidCompletion {
    fun completion(preBidAdInfo: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidAdInfo)
}
