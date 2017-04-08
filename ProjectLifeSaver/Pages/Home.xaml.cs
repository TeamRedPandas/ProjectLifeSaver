using Windows.UI.Xaml;

namespace ProjectLifeSaver.Pages
{
    public sealed partial class Home : PageBase
    {
        public Home()
        {
            Name = "Home";
            InitializeComponent();
        }

        private async void Bt_Help_Click(object sender, RoutedEventArgs e)
        {
            await MainPage.Current.GetHelp();
        }
    }
}
