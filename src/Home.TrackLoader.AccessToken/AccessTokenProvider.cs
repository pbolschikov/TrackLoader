using System;
using System.Threading;
using System.Threading.Tasks;
using Castle.Windsor;
using Home.EslLoader;
using Home.TrackLoader.AccessToken.Windsor;

namespace Home.TrackLoader.AccessToken
{
    public sealed class AccessTokenProvider : IAccessTokenProvider
    {
        public Task<string> GetAccessToken()
        {
            var container = new WindsorContainer();
            container.Install(new AccessTokenInstaller());
            var tcs = new TaskCompletionSource<string>();
            var thread = new Thread(() =>
                                    {
                                        IMainWindowViewModel viewModel = null;
                                        try
                                        {
                                            viewModel = container.Resolve<IMainWindowViewModel>();
                                            tcs.SetResult(viewModel.ShowDilalog().GetValueOrDefault()
                                                ? viewModel.AccessToken
                                                : null);
                                        }
                                        catch (Exception ex)
                                        {
                                            tcs.SetException(ex);
                                        }
                                        finally
                                        {
                                            if (viewModel != null)
                                            {
                                                container.Release(viewModel);  
                                            }
                                        }
                                    });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
    }
}