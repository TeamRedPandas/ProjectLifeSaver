using ProjectLifeSaver.Models;
using Windows.UI.Xaml;

namespace ProjectLifeSaver.Pages
{
    public sealed partial class Profile : PageBase
    {
        private ProfileData ProfileData
        {
            get { return ProfileData.Current; }
        }

        public Profile()
        {
            Name = "Profile";
            InitializeComponent();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Bo_ProfilePicture.Height = Bo_ProfilePictureBackground.ActualHeight;
        }
    }
}
