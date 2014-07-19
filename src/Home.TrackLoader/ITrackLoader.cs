using System.Threading.Tasks;

namespace Home.TrackLoader
{
    interface ITrackLoader
    {
        Task Load(string url, string outputFile);
    }
}
