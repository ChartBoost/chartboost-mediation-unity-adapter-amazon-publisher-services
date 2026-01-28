# Chartboost Mediation Unity SDK - Amazon Publisher Services Adapter

Provides a list of externally configurable properties pertaining to the partner SDK that can be retrieved and set by publishers. 

Dependencies for the adapter are now embedded in the package, and can be found at `com.chartboost.mediation.unity.adapter.amazon-publisher-services/Editor/AmazonPublisherServicesAdapterDependencies.xml`.

Ad adapter for Amazon Publisher Services, to be utilized along the APS Unity Plugin.

# Installation
This package is meant to be integrating when using Amazon Publisher Services as an ad adapter.

### Using the public [npm registry](https://www.npmjs.com/search?q=com.chartboost.mediation.unity.adapter.amazon-publisher-services)

In order to add the Chartboost Mediation Unity SDK - Amazon Publisher Services Adapter to your project using the npm package, add the following to your Unity Project's ***manifest.json*** file. The scoped registry section is required in order to fetch packages from the NpmJS registry.

```json
"dependencies": {
    "com.chartboost.mediation.unity.adapter.amazon-publisher-services": "5.2.2",
    ...
},
"scopedRegistries": [
{
    "name": "NpmJS",
    "url": "https://registry.npmjs.org",
    "scopes": [
    "com.chartboost"
    ]
}
]
```

### Using the public [NuGet package](https://www.nuget.org/packages/Chartboost.CSharp.Mediation.Unity.Adapter.AmazonPublisherServices)

To add the Chartboost Mediation Unity SDK - Amazon Publisher Services Adapter to your project using the NuGet package, you will first need to add the [NugetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) package into your Unity Project.

This can be done by adding the following to your Unity Project's ***manifest.json***

```json
  "dependencies": {
    "com.github-glitchenzo.nugetforunity": "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
    ...
  },
```

Once <code>NugetForUnity</code> is installed, search for `Chartboost.CSharp.Mediation.Unity.Adapter.AmazonPublisherServices` in the search bar of Nuget Explorer window(Nuget -> Manage Nuget Packages).
You should be able to see the `Chartboost.CSharp.Mediation.Unity.Adapter.AmazonPublisherServices` package. Choose the appropriate version and install.

# Demo App
A demo application is available in the following [repository](https://github.com/ChartBoost/chartboost-mediation-unity-adapter-amazon-publisher-services-demo).

# AndroidManifest.xml Permissions & Activities

The following permissions must be present in your manifest file:

```xml
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
```

If you wish to pass geo location information, please include the following permissions in your manifest file:

```xml
<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
```

Add the following under the <application> section of your manifest file:

```xml
<activity android:name="com.amazon.device.ads.DTBInterstitialActivity"/>
<activity android:name="com.amazon.device.ads.DTBAdActivity"/>
```

# Dependencies

> **Note** \
> We recommend to delete `Assets/Amazon/Scripts/Editor/AmazonDependencies.xml` and use the `AmazonPublisherServicesAdapterDependencies.xml` file included in the APS Adapter.

## Using AmazonDependencies.xml and Adding AmazonPublisherServicesSDK to All Targets

In some scenarios your application might fail to load the `AmazonPublisherServicesSDK`, this is due to the default dependency included in the APS Unity plugin not adding the `AmazonPublisherServicesSDK` `addToAllTargets` flag.

To resolve, open, `Assets/Amazon/Scripts/Editor/AmazonDependencies.xml`

And change: 

```xml
<iosPod name="AmazonPublisherServicesSDK" version="~> X.Y.Z"/>
```

to 

```xml
<iosPod name="AmazonPublisherServicesSDK" version="~> X.Y.Z" addToAllTargets="true"/>
```

# Usage

## IPartnerAdapterConfiguration Properties

```csharp

// AdapterUnityVersion - The partner adapter Unity version, e.g: 5.0.0
Debug.Log($"Adapter Unity Version: {AmazonPublisherServicesAdapter.AdapterUnityVersion}");

// AdapterNativeVersion - The partner adapter version, e.g: 5.9.9.5.0
Debug.Log($"Adapter Native Version: {AmazonPublisherServicesAdapter.AdapterNativeVersion}");

// PartnerSDKVersion - The partner SDK version, e.g: aps-android-9.9.5
Debug.Log($"Partner SDK Version: {AmazonPublisherServicesAdapter.PartnerSDKVersion}");

// PartnerIdentifier - The partner ID for internal uses, e.g: amazon_aps
Debug.Log($"Partner Identifier: {AmazonPublisherServicesAdapter.PartnerIdentifier}");

// PartnerDisplayName - The partner name for external uses, e.g: Amazon Publisher Services
Debug.Log($"Partner Display Name: {AmazonPublisherServicesAdapter.PartnerDisplayName}");
```
## Test Mode
To enable test mode for the Amazon Publisher Services adapter, the following property has been made available:

```csharp
AmazonPublisherServicesAdapter.TestMode = true;
```

## Verbose Logging
To enable verbose logging for the Amazon Publisher Services adapter, the following property has been made available:

```csharp
AmazonPublisherServicesAdapter.VerboseLogging = true;
```

## Creating a Pre-Bidding Listener

Chartboost is not permitted to wrap the Amazon APS initialization or bid request methods directly. The adapter handles APS initialization and prebidding only when the managed prebidding flag is enabled. For more information please contact the Amazon APS support team at https://aps.amazon.com/aps/contact-us/.

As such, developers are meant to follow the guidelines in the [APS Unity Documentation](https://ams.amazon.com/webpublisher/uam/docs/aps-mobile/unity).

In order to pass prebidding information back to the Chartboost Mediation Unity SDK we have provided the following.

Developers will have to call the APS Plugin and pass the information to the Chartboost Mediation SDK, the `PreBiddingListener` can be used to organize your logic and easily pass the required data, see example below:

```csharp
public class CustomAmazonPublisherServicesPreBiddingListener : PreBiddingListener
    {
        private const string TAG = "[APS PreBidding Listener]";
        
        public override Task<AmazonPublisherServicesAdapterPreBidAdInfo> OnPreBid(AmazonPublisherServicesAdapterPreBidRequest request)
        {
            AdRequest adRequest = null;
            var amazonSetting = request.AmazonSettings;
            var width = amazonSetting.Width ?? 0;
            var height = amazonSetting.Height ?? 0;
            var amazonPlacement = amazonSetting.PartnerPlacement;
                 
            Debug.Log($"{TAG} Format: {request.Format}, Chartboost Placement: {request.ChartboostPlacement}, Amazon Placement: {amazonPlacement}");

            switch (request.Format)
            {
                case "rewarded":
                    adRequest = new APSVideoAdRequest(width, height, amazonPlacement);
                    break;

                case "interstitial":
                case "rewarded_interstitial":
                    adRequest = new APSInterstitialAdRequest(amazonPlacement);
                    break;

                case "banner":
                case "adaptive_banner":
                    adRequest = new APSBannerAdRequest(width, height, amazonPlacement);
                    break;
                
                default:
                    Debug.LogWarning($"{TAG} Specified type is not valid, returning null values.");
                    return Task.FromResult(new AmazonPublisherServicesAdapterPreBidAdInfo(null, null));
            }

            var taskCompletionSource = new TaskCompletionSource<AmazonPublisherServicesAdapterPreBidAdInfo>();
            
            adRequest.onSuccess += response =>
            {
                Debug.Log($"{TAG} Response succeeded for: CBP: {request.ChartboostPlacement} - AMZP: {amazonPlacement}!");
                #if UNITY_IOS
                taskCompletionSource.SetResult(new AmazonPublisherServicesAdapterPreBidAdInfo(response.GetPricePoint(), response.GetMediationHints()));
                #elif UNITY_ANDROID
                taskCompletionSource.SetResult(new AmazonPublisherServicesAdapterPreBidAdInfo(response.GetPricePoint(), response.GetBidInfo()));
                #else
                taskCompletionSource.SetResult(new AmazonPublisherServicesAdapterPreBidAdInfo(null, null));   
                #endif
            };

            adRequest.onFailedWithError += error =>
            {
                Debug.LogError($"{TAG} Failed with Error: {error.GetMessage()} and Code: {error.GetCode()}");
                taskCompletionSource.SetResult(new AmazonPublisherServicesAdapterPreBidAdInfo(null, null));
            };

            adRequest.LoadAd();
            return taskCompletionSource.Task;
        }
    }
```

### Setting the Pre-Bidding Listener

```csharp
// Publisher Initializes the AmazonPubliserServices Unity Plugin
Amazon.Initialize("YOUR_AMAZON_API_KEY_GOES_HERE");

// Create instance for your Custom Pre-Bidding Listener
AmazonPublisherServicesAdapter.PreBiddingListener = new CustomAmazonPublisherServicesPreBiddingListener();
```
