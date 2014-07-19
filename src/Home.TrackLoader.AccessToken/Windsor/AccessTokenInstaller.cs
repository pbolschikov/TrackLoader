using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Home.TrackLoader.AccessToken.Windsor
{
    internal sealed class AccessTokenInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<VkSettings>(),
                Component.For<IView>().ImplementedBy<MainWindow>().LifestyleTransient(),
                Component.For<IMainWindowModel>().ImplementedBy<MainWindowModel>(),
                Component.For<IMainWindowViewModel>().ImplementedBy<MainWindowViewModel>());
        }
    }
}