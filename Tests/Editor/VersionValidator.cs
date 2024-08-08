using Chartboost.Editor;
using Chartboost.Logging;
using Chartboost.Mediation.AmazonPublisherServices;
using NUnit.Framework;

namespace Chartboost.Tests.Editor
{
    internal class VersionValidator
    {
        private const string UnityPackageManagerPackageName = "com.chartboost.mediation.unity.adapter.amazon-publisher-services";
        private const string NuGetPackageName = "Chartboost.CSharp.Mediation.Unity.Adapter.AmazonPublisherServices";
        
        [SetUp]
        public void SetUp() 
            => LogController.LoggingLevel = LogLevel.Debug;
            
        [Test]
        public void ValidateVersion() 
            => VersionCheck.ValidateVersions(UnityPackageManagerPackageName, NuGetPackageName, AmazonPublisherServicesAdapter.AdapterUnityVersion);
    }
}
