using Chartboost.Logging;
using Chartboost.Mediation.AmazonPublisherServices;
using Chartboost.Tests.Runtime;
using NUnit.Framework;

namespace Chartboost.Tests
{
    internal class AmazonPublisherServicesAdapterTests
    {
        [SetUp]
        public void SetUp() 
            => LogController.LoggingLevel = LogLevel.Debug;

        [Test]
        public void AdapterNativeVersion()
            => TestUtilities.TestStringGetter(() => AmazonPublisherServicesAdapter.AdapterNativeVersion);
        
        [Test]
        public void PartnerSDKVersion()
            => TestUtilities.TestStringGetter(() => AmazonPublisherServicesAdapter.PartnerSDKVersion);
        
        [Test]
        public void PartnerIdentifier()
            => TestUtilities.TestStringGetter(() => AmazonPublisherServicesAdapter.PartnerIdentifier);   
        
        [Test]
        public void PartnerDisplayName()
            => TestUtilities.TestStringGetter(() => AmazonPublisherServicesAdapter.PartnerDisplayName);

        [Test]
        public void TestMode() 
            => TestUtilities.TestBooleanAccessor(() => AmazonPublisherServicesAdapter.TestMode, value=> AmazonPublisherServicesAdapter.TestMode = value);

        [Test]
        public void VerboseLogging() 
            => TestUtilities.TestBooleanAccessor(() => AmazonPublisherServicesAdapter.VerboseLogging, value=> AmazonPublisherServicesAdapter.VerboseLogging = value);
    }
}
