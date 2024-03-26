using System.Runtime.CompilerServices;
using Chartboost.Mediation.AmazonPublisherServices;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
[assembly: InternalsVisibleTo(AssemblyInfo.AmazonPublisherServicesAssemblyInfoAndroid)]
[assembly: InternalsVisibleTo(AssemblyInfo.AmazonPublisherServicesAssemblyInfoIOS)]

namespace Chartboost.Mediation.AmazonPublisherServices
{
    internal class AssemblyInfo
    {
        public const string AmazonPublisherServicesAssemblyInfoAndroid = "Chartboost.Mediation.AmazonPublisherServices.Android";
        public const string AmazonPublisherServicesAssemblyInfoIOS = "Chartboost.Mediation.AmazonPublisherServices.IOS";
    }
}
