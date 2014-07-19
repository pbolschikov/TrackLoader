using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Home.TrackLoader
{
    internal sealed class TrackLoader : ITrackLoader
    {
        public async Task Load(string url, string outputFile)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri(url));
            using (var netStream = await response.Content.ReadAsStreamAsync())
            {
                const int bufferSize = 1024;
                var buffer = new byte[bufferSize];
                using (
                    var fileStream = new FileStream(Path.Combine(outputFile, outputFile),
                        FileMode.Create,
                        FileAccess.Write, FileShare.Read, bufferSize, FileOptions.Asynchronous))
                {
                    int size;
                    while ((size = await netStream.ReadAsync(buffer, 0, bufferSize)) != 0)
                    {
                        await fileStream.WriteAsync(buffer.Take(size).ToArray(), 0, size);
                    }
                }
            }
        }
    }
}