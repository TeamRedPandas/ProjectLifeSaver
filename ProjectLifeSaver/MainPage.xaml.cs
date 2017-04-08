using ProjectLifeSaver.Models;
using ProjectLifeSaver.Pages;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UWPHelper.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectLifeSaver
{
    public sealed partial class MainPage : Page
    {
        public static readonly DependencyProperty AIOverlayVisibilityProperty = DependencyProperty.Register(nameof(AIOverlayVisibility), typeof(Visibility), typeof(MainPage), new PropertyMetadata(Visibility.Collapsed, OnAIOverlayVisibilityPropertyChanged));

        public static MainPage Current { get; private set; }

        private readonly ApiAiHelper apiAiHelper = new ApiAiHelper();

        public readonly MediaElement element = new MediaElement { AutoPlay = false };
        
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
            TB_Message.Focus(FocusState.Programmatic);

            return apiAiHelper.GetResponse();
        }
        
        private async void SendMessage(object sender, object e)
        {
            await apiAiHelper.GetResponse();

            TB_Message.Text = "";
            TB_Message.Focus(FocusState.Programmatic);

            LV_Messages.ScrollIntoView(LV_Messages.Items[LV_Messages.Items.Count - 1]);
        }

        private void APi_Main_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (PivotItem pivotItem in APi_Main.Items)
            {
                Frame contentFrame = new Frame();
                contentFrame.Navigate(Type.GetType("ProjectLifeSaver.Pages." + pivotItem.Name.Remove(0, 4)));
                pivotItem.Content = contentFrame;

                pivotItem.Header = ((PageBase)contentFrame.Content).Name.ToUpper();
                pivotItem.RequestedTheme = ElementTheme.Light;
            }
        }

        private static async void OnAIOverlayVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((Visibility)e.NewValue == Visibility.Visible)
            {
                await BarsHelper.Current.SetTitleBarColorModeAsync(BarsHelperColorMode.ThemedGray);
                await BarsHelper.Current.SetStatusBarColorModeAsync(BarsHelperColorMode.ThemedGray);
            }
            else
            {
                await App.Current.SetDefaultBarsColors();
            }
        }
    }
}
