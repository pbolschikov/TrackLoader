using Castle.Windsor;
using Home.TrackLoader.AccessToken.Windsor;
using Home.TrackLoader.Windsor;

namespace Home.TrackLoader.Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new TrackLoaderInstaller(), new AccessTokenInstaller());
            var application = container.Resolve<ITrackLoaderApplication>();
            application.Start().Wait();
        }
    }
}