using System.Threading.Tasks;

namespace Home.EslLoader
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessToken();
    }
}