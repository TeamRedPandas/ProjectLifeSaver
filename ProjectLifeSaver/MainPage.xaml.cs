using Microsoft.Graphics.Canvas.Effects;
using ProjectLifeSaver.Models;
using ProjectLifeSaver.Pages;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace ProjectLifeSaver
{
    public sealed partial class MainPage : Page
    {
        public static MainPage Current { get; private set; }

        public ObservableCollection<MessageData> Log;
        public ApiAiHelper Requester;
        //public List

        public Visibility AiLogVisibility
        {
            get { return (Visibility)GetValue(AiLogVisibilityProperty); }
            set { SetValue(AiLogVisibilityProperty, value); }
        }
        
        public static readonly DependencyProperty AiLogVisibilityProperty = DependencyProperty.Register(nameof(AiLogVisibility),
                                                                                                        typeof(Visibility),
                                                                                                        typeof(MainPage),
                                                                                                        new PropertyMetadata(Visibility.Collapsed));
        
        public MainPage()
        {
            Current = this;
            InitializeComponent();

            Log = new ObservableCollection<MessageData>();
            LV_ApiAiLog.ItemsSource = Log;

            Requester = new ApiAiHelper();
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

        private async void Test(object sender, RoutedEventArgs e)
        {
            await Requester.GetResponse();
        }
    }
}
