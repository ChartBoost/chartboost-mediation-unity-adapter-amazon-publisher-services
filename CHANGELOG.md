# Changelog
All notable changes to this project will be documented in this file using the standards as defined at [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0).

### Version 4.0.1 *(20204-03-27)*
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
