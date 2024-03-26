package com.chartboost.mediation.unity.aps

import com.chartboost.mediation.amazonpublisherservicesadapter.AmazonPublisherServicesAdapter

interface APSPreBidCompletion {
    fun completion(preBidAdInfo: AmazonPublisherServicesAdapter.AmazonPublisherServicesAdapterPreBidAdInfo)
}
