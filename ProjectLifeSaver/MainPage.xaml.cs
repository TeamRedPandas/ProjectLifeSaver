using ProjectLifeSaver.Pages;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectLifeSaver
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void APi_Main_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (PivotItem pivotItem in APi_Main.Items)
            {
                try
                {
                    Frame contentFrame = new Frame();
                    contentFrame.Navigate(Type.GetType("ProjectLifeSaver.Pages." + pivotItem.Name.Remove(0, 4)));
                    pivotItem.Content = contentFrame;

                    pivotItem.Header = ((PageBase)contentFrame.Content).Name.ToUpper();
                    pivotItem.RequestedTheme = ElementTheme.Light;
                }
                catch
                {
                    // Some pages are not yet implemented
                    pivotItem.Header = pivotItem.Name.Remove(0, 4).ToUpper();
                }
            }
        }
    }
}
