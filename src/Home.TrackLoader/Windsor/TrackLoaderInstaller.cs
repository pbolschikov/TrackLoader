using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Home.TrackLoader.Windsor
{
    public sealed class TrackLoaderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITrackLoaderApplication>().ImplementedBy<TrackLoaderApplication>(),
                Component.For<TrackLoaderConfiguration>().Instance(TrackLoaderConfiguration.Default),
                Component.For<ITrackLoader>().ImplementedBy<TrackLoader>(),
                Component.For<ITrackListProvider>().ImplementedBy<VkTrackListProvider>());
        }
    }
}