using System.Collections.Generic;
using System.Threading.Tasks;

namespace Home.TrackLoader
{
    internal interface ITrackListProvider
    {
        Task<IEnumerable<TrackInfo>> GetTracks();
    }
}