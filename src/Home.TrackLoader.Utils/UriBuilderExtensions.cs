using System;

namespace Home.TrackLoader.Utils
{
    public static class UriBuilderExtensions
    {
        public static UriBuilder Append(this UriBuilder uriBuilder, string parameter, string value)
        {
            if (uriBuilder == null)
            {
                throw new ArgumentNullException("uriBuilder");
            }
            var queryToAppend = Uri.EscapeUriString(string.Format("{0}={1}", parameter, value));
            if (uriBuilder.Query.Length > 1)
            {
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + queryToAppend;
            }
            else
            {
                uriBuilder.Query = queryToAppend;
            }
            return uriBuilder;
        }
    }
}