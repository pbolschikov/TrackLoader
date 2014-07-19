using System.Threading.Tasks;

namespace Home.TrackLoader
{
    public interface IVkSessionInfoProvider
    {
        Task<SessionInfo> GetAccessToken();
    }

    public class SessionInfo
    {
        private readonly string m_AccessToken;
        private readonly string m_UserId;

        public SessionInfo(string accessToken, string userId)
        {
            m_AccessToken = accessToken;
            m_UserId = userId;
        }

        public string AccessToken
        {
            get { return m_AccessToken; }
        }

        public string UserId
        {
            get { return m_UserId; }
        }

        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(m_AccessToken) && !string.IsNullOrEmpty(m_UserId); }
        }
    }
}