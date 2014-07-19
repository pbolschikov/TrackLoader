using System;
using System.Threading;
using System.Threading.Tasks;

namespace Home.TrackLoader.AccessToken
{
    public sealed class VkSessionInfoProvider : IVkSessionInfoProvider
    {
        private readonly IViewModelFactory m_ViewModelFactory;

        public VkSessionInfoProvider(IViewModelFactory viewModelFactory)
        {
            m_ViewModelFactory = viewModelFactory;
        }

        public Task<SessionInfo> GetAccessToken()
        {
            var tcs = new TaskCompletionSource<SessionInfo>();
            var thread = new Thread(() =>
                                    {
                                        IMainWindowViewModel viewModel = null;
                                        try
                                        {
                                            viewModel = m_ViewModelFactory.Create();
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
                                                m_ViewModelFactory.Release(viewModel);  
                                            }
                                        }
                                    });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
    }

    public interface IViewModelFactory
    {
        IMainWindowViewModel Create();
        void Release(IMainWindowViewModel viewModel);
    }
}