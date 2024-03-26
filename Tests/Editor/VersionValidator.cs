using Chartboost.Editor;
using NUnit.Framework;

namespace Chartboost.Mediation.AmazonPublisherServices.Tests.Editor
{
    public class VersionValidator
    {
        private const string UnityPackageManagerPackageName = "com.chartboost.mediation.unity.aps";
        private const string NuGetPackageName = "Chartboost.CSharp.Mediation.Unity.Adapter.AmazonPublisherServices";
        
        [Test]
        public void ValidateVersion() 
            => VersionCheck.ValidateVersions(UnityPackageManagerPackageName, NuGetPackageName, AmazonPublisherServicesAdapter.Version);
    }
}
