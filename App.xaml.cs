using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace XboxDownloader
{
    sealed partial class App : Application
    {
        public App() { InitializeComponent(); }
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Window.Current.Content = new MainPage();
            Window.Current.Activate();
        }
    }
}
