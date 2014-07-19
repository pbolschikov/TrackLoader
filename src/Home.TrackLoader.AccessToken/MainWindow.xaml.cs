namespace Home.TrackLoader.AccessToken
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IView
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public IMainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}