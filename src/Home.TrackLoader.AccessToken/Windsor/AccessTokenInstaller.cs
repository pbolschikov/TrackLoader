using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Home.TrackLoader.AccessToken.Windsor
{
    public sealed class AccessTokenInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IVkSessionInfoProvider>().ImplementedBy<VkSessionInfoProvider>(),
                Component.For<IViewModelFactory>().AsFactory(),
                Component.For<VkSettings>(),
                Component.For<IView>().ImplementedBy<MainWindow>().LifestyleTransient(),
                Component.For<IMainWindowModel>().ImplementedBy<MainWindowModel>(),
                Component.For<IMainWindowViewModel>().ImplementedBy<MainWindowViewModel>());
        }
    }
}