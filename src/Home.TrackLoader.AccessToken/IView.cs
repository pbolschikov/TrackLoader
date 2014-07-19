namespace Home.TrackLoader.AccessToken
{
    internal interface IView
    {
        bool? DialogResult { get; set; }
        bool? ShowDialog();
        IMainWindowViewModel ViewModel { set; }
    }
}