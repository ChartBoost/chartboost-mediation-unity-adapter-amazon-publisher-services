using System;
using NUnit.Framework;
using UnityEngine;

namespace Chartboost.Mediation.AmazonPublisherServices.Tests.Editor
{
    public class AmazonPublisherServicesAdapterTests
    {
        [Test]
        public void TestMode() 
            => TestBooleanAccessor(() => AmazonPublisherServicesAdapter.TestMode, value=> AmazonPublisherServicesAdapter.TestMode = value);

        [Test]
        public void VerboseLogging() 
            => TestBooleanAccessor(() => AmazonPublisherServicesAdapter.VerboseLogging, value=> AmazonPublisherServicesAdapter.VerboseLogging = value);

        private static void TestBooleanAccessor(Func<bool> get, Func<bool, bool> set)
        {
            var initial = get();
            Debug.Log(initial);
            Assert.False(initial);
            var change = set(true);
            Debug.Log(change);
            Assert.True(change);
            set(false);
            Assert.False(get());
        }
    }
}
