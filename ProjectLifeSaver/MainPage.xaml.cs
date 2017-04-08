using ProjectLifeSaver.Models;
using ProjectLifeSaver.Pages;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UWPHelper.UI;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ProjectLifeSaver
{
    public sealed partial class MainPage : Page
    {
        public static readonly DependencyProperty AIOverlayVisibilityProperty = DependencyProperty.Register(nameof(AIOverlayVisibility), typeof(Visibility), typeof(MainPage), new PropertyMetadata(Visibility.Collapsed, OnAIOverlayVisibilityPropertyChanged));

        public static MainPage Current { get; private set; }

        private readonly ApiAiHelper apiAiHelper = new ApiAiHelper();
        
        public ObservableCollection<MessageData> AiMessages { get; }
        private Visibility AIOverlayVisibility
        {
            get { return (Visibility)GetValue(AIOverlayVisibilityProperty); }
            set { SetValue(AIOverlayVisibilityProperty, value); }
        }
        
        public MainPage()
        {
            Current     = this;
            AiMessages  = new ObservableCollection<MessageData>();

            InitializeComponent();
        }

        public Task GetHelp()
        {
            AIOverlayVisibility = Visibility.Visible;
            return apiAiHelper.GetResponse();
        }
        
        private async void SendMessage(object sender, object e)
        {
            await apiAiHelper.GetResponse();
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

        private static async void OnAIOverlayVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((Visibility)e.NewValue == Visibility.Visible)
            {
                BarsHelperColorsSetterColorInfo barsColorInfo = new BarsHelperColorsSetterColorInfo(Colors.Black, Colors.White);
                await App.Current.SetBarsColors(barsColorInfo);
            }
            else
            {
                await App.Current.SetDefaultBarsColors();
            }
        }
    }
}
