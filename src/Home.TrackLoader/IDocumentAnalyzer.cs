using System.Collections.Generic;
using System.Threading.Tasks;

namespace Home.EslLoader
{
    internal interface IDocumentAnalyzer
    {
        Task<IEnumerable<TrackInfo>> GetTracks(string eslUrl);
    }
}