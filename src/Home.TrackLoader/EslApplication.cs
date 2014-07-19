using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Home.EslLoader
{
    internal sealed class EslApplication : IEslApplication
    {
        private readonly IDocumentAnalyzer m_DocumentAnalyzer;
        private readonly EslConfiguration m_EslConfiguration;
        private readonly ITrackLoader m_TrackLoader;
        private readonly SemaphoreSlim m_SemaphoreSlim = new SemaphoreSlim(1);

        public EslApplication(IDocumentAnalyzer documentAnalyzer, ITrackLoader trackLoader,
            EslConfiguration eslConfiguration)
        {
            m_DocumentAnalyzer = documentAnalyzer;
            m_TrackLoader = trackLoader;
            m_EslConfiguration = eslConfiguration;
        }

        public async Task Start()
        {
            var trackInfos =
                await m_DocumentAnalyzer.GetTracks(m_EslConfiguration.EslUrl);
            await Task.WhenAll(trackInfos.Select(downloadTrack));
        }

        private async Task downloadTrack(TrackInfo trackInfo)
        {
            var fileName = Path.Combine(m_EslConfiguration.OutputFolder, trackInfo.Name);
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