using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Home.EslLoader.Windsor
{
    internal sealed class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEslApplication>().ImplementedBy<EslApplication>(),
                Component.For<EslConfiguration>().Instance(EslConfiguration.Default),
                Component.For<ITrackLoader>().ImplementedBy<TrackLoader>(),
                Component.For<IDocumentAnalyzer>().ImplementedBy<DocumentAnalyzer>());
        }
    }
}