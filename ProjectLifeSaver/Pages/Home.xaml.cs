namespace ProjectLifeSaver.Pages
{
    public sealed partial class Home : PageBase
    {
        public Home()
        {
            Name = "Home";
            InitializeComponent();
        }

        private async void Bt_Help_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainPage.Current.AiLogVisibility = Windows.UI.Xaml.Visibility.Visible;
            await MainPage.Current.Requester.GetResponse();
        }
    }
}
