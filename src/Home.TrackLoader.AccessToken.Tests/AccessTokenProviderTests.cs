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
            var provider = new VkSessionInfoProvider();
            Console.WriteLine(provider.GetAccessToken().Result);
        }
    }
}