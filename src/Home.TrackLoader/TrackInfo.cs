namespace Home.TrackLoader
{
    internal sealed class TrackInfo
    {
        private readonly string m_Url;
        private readonly string m_Name;

        public TrackInfo(string url, string name)
        {
            m_Url = url;
            m_Name = name;
        }

        public string Url
        {
            get { return m_Url; }
        }

        public string Name
        {
            get { return m_Name; }
        }
    }
}