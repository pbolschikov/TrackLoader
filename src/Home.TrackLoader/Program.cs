using Castle.Windsor;
using Home.EslLoader.Windsor;

namespace Home.EslLoader
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            var installer = new ApplicationInstaller();
            container.Install(installer);
            var application = container.Resolve<IEslApplication>();
            application.Start().Wait();
        }
    }
}