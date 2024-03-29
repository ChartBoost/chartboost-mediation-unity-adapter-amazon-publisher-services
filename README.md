# Amazon Publisher Services Unity Ad Adapter

Ad adapter for Amazon Publisher Services, to be utilized along the APS Unity Plugin.

# Installation
This package is meant to be integrating when using Amazon Publisher Services as an ad adapter.

### Using the public [npm registry](https://www.npmjs.com/search?q=com.chartboost.mediation.unity.aps)

In order to add the Chartboost Mediation Unity SDK - AmazonPublisherServices Adapter to your project using the npm package, add the following to your Unity Project's ***manifest.json*** file. The scoped registry section is required in order to fetch packages from the NpmJS registry.

```json
"dependencies": {
    "com.chartboost.mediation.unity.aps": "4.0.1",
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

To add the Chartboost Mediation Unity SDK - AmazonPublisherServices Adapter to your project using the NuGet package, you will first need to add the [NugetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) package into your Unity Project.

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

# Dependencies

> **Note** \
> We recommend to delete `Assets/Amazon/Scripts/Editor/AmazonDependencies.xml` and use the `AmazonPublisherServicesAdapterDependencies.xml` file included in the APS Adapter.

## Using AmazonDependencies.xml and Adding AmazonPublisherServicesSDK to All Targets

In some scenarios your application might fail to load the `AmazonPublisherServicesSDK`, this is due to the default dependency included in the APS Unity plugin not adding the `AmazonPublisherServicesSDK` `addToAllTargets` flag.

To resolve, open, `Assets/Amazon/Scripts/Editor/AmazonDependencies.xml`

And change: 

```xml
<iosPod name="AmazonPublisherServicesSDK" version="~> 4.7.2"/>
```

to 

```xml
<iosPod name="AmazonPublisherServicesSDK" version="~> 4.7.2" addToAllTargets="true"/>
```

# Usage of AmazonPublisherServicesAdapter APIs

Chartboost is not permitted to wrap the Amazon APS initialization or bid request methods directly. The adapter handles APS initialization and prebidding only when the managed prebidding flag is enabled. For more information please contact the Amazon APS support team at https://aps.amazon.com/aps/contact-us/.

As such, developers are meant to follow the guidelines in the [APS Unity Documentation](https://ams.amazon.com/webpublisher/uam/docs/aps-mobile/unity).

In order to pass prebidding information back to the Chartboost Mediation Unity SDK we have provided the following example: 


### Creating a Pre-Bidding Listener

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

// Plugins + Adapter Can be configured as needed.
Amazon.EnableTesting(true);
Amazon.EnableLogging(true);
AmazonPublisherServicesAdapter.TestMode = true;
AmazonPublisherServicesAdapter.VerboseLogging = true;

// Create instance for your Custom Pre-Bidding Listener
AmazonPublisherServicesAdapter.PreBiddingListener = new CustomAmazonPublisherServicesPreBiddingListener();

// Initialize the Chartboost Mediation Unity SDK.
ChartboostMediation.StartWithOptions(ChartboostMediationSettings.AppId, ChartboostMediationSettings.AppSignature);
```
