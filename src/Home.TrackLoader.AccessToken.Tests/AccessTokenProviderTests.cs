using System;
using NUnit.Framework;

namespace Home.TrackLoader.AccessToken.Tests
{
    [TestFixture]
    public sealed class AccessTokenProviderTests
    {
        [Test]
        public void GetAccessTokenTest()
        {
            var provider = new AccessTokenProvider();
            Console.WriteLine(provider.GetAccessToken().Result);
        }
    }
}