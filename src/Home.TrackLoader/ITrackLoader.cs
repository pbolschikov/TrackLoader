using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Home.EslLoader
{
    interface ITrackLoader
    {
        Task Load(string url, string outputFile);
    }
}
