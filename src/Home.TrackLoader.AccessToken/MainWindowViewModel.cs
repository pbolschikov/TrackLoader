using System;
using GalaSoft.MvvmLight;

namespace Home.TrackLoader.AccessToken
{
    internal sealed class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private readonly IView m_View;
        private readonly IMainWindowModel m_MainWindowModel;

        public MainWindowViewModel(IView view, IMainWindowModel mainWindowModel)
        {
            m_View = view;
            m_MainWindowModel = mainWindowModel;
        }

        public Uri Source
        {
            get { return m_MainWindowModel.Source; }
            set
            {
                m_MainWindowModel.Source = value;
                if (m_MainWindowModel.AccessToken.IsValid)
                {
                    m_View.DialogResult = true;
                }
            }
        }

        public SessionInfo AccessToken
        {
            get { return m_MainWindowModel.AccessToken; }
        }

        public bool? ShowDilalog()
        {
            m_View.ViewModel = this;
            return m_View.ShowDialog();
        }
    }

    public interface IMainWindowViewModel
    {
        SessionInfo AccessToken { get; }
        bool? ShowDilalog();
    }
}