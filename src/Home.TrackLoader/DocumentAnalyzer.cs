using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Home.EslLoader
{
    internal sealed class DocumentAnalyzer : IDocumentAnalyzer
    {
        public async Task<IEnumerable<TrackInfo>> GetTracks(string eslUrl)
        {
            var trackInfos = Enumerable.Empty<TrackInfo>();
            var eslUri = new Uri(eslUrl);
            while (true)
            {
                var doc = new HtmlDocument();
                var client = new HttpClient();
                var stream = await client.GetStreamAsync(eslUri);
                doc.Load(stream);
                trackInfos = trackInfos.Concat(parseDocument(doc).ToArray());
                var next = doc.DocumentNode.SelectNodes("//div[@class='play_btn fl_l']/input");
                if (next == null || next.Count == 0)
                {
                    break;
                }
                eslUri = new Uri(eslUri, next[0].Attributes["href"].Value);
            }
            return trackInfos;
        }

        private static IEnumerable<TrackInfo> parseDocument(HtmlDocument htmlDocument)
        {
            foreach (HtmlNode link in htmlDocument.DocumentNode.SelectNodes(@"//div[@class='play_btn fl_l']/input"))
            {
                string lessonName = link.InnerText.Trim();
                string downloadLink = link.ParentNode.ParentNode.SelectNodes(".//a[text()='Download Podcast']")[0].Attributes["href"].Value;
                yield return new TrackInfo(downloadLink, Path.ChangeExtension(lessonName, "mp3"));
            }
        }
    }
}