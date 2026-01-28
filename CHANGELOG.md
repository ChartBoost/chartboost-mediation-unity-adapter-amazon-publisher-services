# Changelog
All notable changes to this project will be documented in this file using the standards as defined at [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0).

### Version 5.2.2 *(2026-01-21)*
This version of the Amazon APS Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-mediation-adapter-amazon-publisher-services:5.11.1.+`
  * iOS: `ChartboostMediationAdapterAmazonPublisherServices: ~> 5.5.3.0`

### Version 5.2.1 *(2026-01-21)*
This version of the Amazon APS Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-mediation-adapter-amazon-publisher-services:5.11.0.+`
  * iOS: `ChartboostMediationAdapterAmazonPublisherServices: ~> 5.5.3.0`

### Version 5.2.0 *(2026-01-21)*
This version of the Amazon APS Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-mediation-adapter-amazon-publisher-services:5.11.0.+`
  * iOS: `ChartboostMediationAdapterAmazonPublisherServices: ~> 5.5.2.0`

### Version 5.1.0 *(2025-06-02)*
This version of the Amazon APS Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-mediation-adapter-amazon-publisher-services:5.10.1.+`
  * iOS: `ChartboostMediationAdapterAmazonPublisherServices: ~> 5.5.2.0`

### Version 5.0.1 *(2024-10-09)*
This version of the Amazon APS Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-mediation-adapter-amazon-publisher-services:5.9.10.+`
  * iOS: `ChartboostMediationAdapterAmazonPublisherServices: ~> 5.4.10.0`

### Version 5.0.0 *(2024-08-08)*

# Added 
- Support for the following 'Amazon Publisher Services' dependencies. Notice adapter dependencies are optimistic and any patches and hot-fixes will be automatically picked up.:
    * Android: `com.chartboost:chartboost-mediation-adapter-amazon-publisher-services:5.9.10.+`
    * iOS: `ChartboostMediationAdapterAmazonPublisherServices ~> ~> 5.4.9.0`
    
- The following properties have been added in `AmazonPublisherServicesAdapter.cs`
    * `string AdapterUnityVersion`
    * `string AdapterNativeVersion`
    * `string PartnerSDKVersion`
    * `string PartnerIdentifier`
    * `string PartnerDisplayName`
    * `bool TestMode`
    * `bool VerboseLogging`

### Version 4.0.1 *(2024-03-27)*
Bug Fixes:

- Added `<androidPackage spec="com.iabtcf:iabtcf-decoder:2.0.10"/>` to `AmazonPublisherServicesAdapterDependencies.xml`

### Version 4.0.0 *(2024-03-21)*
First version of the Chartboost Mediation Unity SDK - Amazon Publisher Services Adapter.

This package requires the APS Unity Plugin to be integrated in the project in order to be used.

New:

- Added `AmazonPublisherServicesAdapter` class.
- Added `TestMode` mode to setup APS test mode.
- Added `VerboseLogging` to modify adapter logging leve.
- Added `PreBiddingListener` class and property to handle publisher based pre-bidding.
- Added `AmazonSettings` struct to pass required data to the Chartboost Mediation SDK.

Compatible with the following native Android and iOS SDK Versions:

* Android: Amazon Publisher Services `com.amazon.android:aps-sdk:9.9.+`
* iOS: `AmazonPublisherServicesSDK ~> 4.8.0`
