using System;
using System.Web;
using Home.TrackLoader.Utils;

namespace Home.TrackLoader.AccessToken
{
    sealed class MainWindowModel : IMainWindowModel
    {
        public MainWindowModel(VkSettings vkSettings)
        {
            Source = buildVkUri(vkSettings);
        }

        private static Uri buildVkUri(VkSettings vkSettings)
        {
            return new UriBuilder(vkSettings.VkAuthUrl).Append("client_id", vkSettings.ClientId)
                .Append("scope", vkSettings.Scope)
                .Append("redirect_uri", vkSettings.RedirectUri)
                .Append("response_type", vkSettings.ResponseType)
                .Append("display", vkSettings.Display).Uri;
        }


        public SessionInfo AccessToken
        {
            get
            {
                var nvc = HttpUtility.ParseQueryString(Source.Fragment);
                var accessToken = nvc["#access_token"];
                var userId = nvc["user_id"];
                return new SessionInfo(accessToken, userId); 
            }
        }

        public Uri Source { get; set; }
    }
}