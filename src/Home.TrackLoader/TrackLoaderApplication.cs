using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Home.TrackLoader
{
    internal sealed class TrackLoaderApplication : ITrackLoaderApplication
    {
        private readonly ITrackListProvider m_TrackListProvider;
        private readonly TrackLoaderConfiguration m_EslConfiguration;
        private readonly ITrackLoader m_TrackLoader;
        private readonly SemaphoreSlim m_SemaphoreSlim = new SemaphoreSlim(1);

        public TrackLoaderApplication(ITrackListProvider trackListProvider, ITrackLoader trackLoader,
            TrackLoaderConfiguration eslConfiguration)
        {
            m_TrackListProvider = trackListProvider;
            m_TrackLoader = trackLoader;
            m_EslConfiguration = eslConfiguration;
        }

        public async Task Start()
        {
            var trackInfos =
                await m_TrackListProvider.GetTracks();
            await Task.WhenAll(trackInfos.Select(downloadTrack));
        }

        private async Task downloadTrack(TrackInfo trackInfo)
        {
            var fileName = Path.ChangeExtension(Path.Combine(m_EslConfiguration.OutputFolder, trackInfo.Name), "mp3");
            if (File.Exists(fileName))
            {
                return;
            }
            while (true)
            {
                await m_SemaphoreSlim.WaitAsync();
                try
                {
                    await
                        m_TrackLoader.Load(trackInfo.Url, fileName);
                    Console.WriteLine("Download resource completed: {0}; File: {1}", trackInfo.Url, trackInfo.Name);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    m_SemaphoreSlim.Release();
                }
            }
        }
    }
}