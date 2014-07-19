using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Home.TrackLoader.Utils;
using Newtonsoft.Json;

namespace Home.TrackLoader
{
    internal sealed class VkTrackListProvider : ITrackListProvider
    {
        private readonly IVkSessionInfoProvider m_SessionInfoProvider;

        public VkTrackListProvider(IVkSessionInfoProvider sessionInfoProvider)
        {
            m_SessionInfoProvider = sessionInfoProvider;
        }

        public async Task<IEnumerable<TrackInfo>> GetTracks()
        {
            var sessionInfo = await m_SessionInfoProvider.GetAccessToken();
            if (!sessionInfo.IsValid)
            {
                return Enumerable.Empty<TrackInfo>();
            }
            var uri = new UriBuilder(@"https://api.vk.com/method/audio.get").Append("oid", sessionInfo.UserId)
                .Append("need_user", "0")
                .Append("count", "2000")
                .Append("offset", "0")
                .Append("access_token", sessionInfo.AccessToken).Uri;
            var client = new HttpClient();
            var jsonString = await client.GetStringAsync(uri);
            var infoResponse = JsonConvert.DeserializeObject<VkTrackInfoResponse>(jsonString);

            return infoResponse.Response.Select(track => new TrackInfo(track.Url, track.Title)).ToArray();
        }
    }

    internal sealed class VkTrackInfoResponse
    {
        public VkTrackInfo[] Response { get; set; }
    }
}