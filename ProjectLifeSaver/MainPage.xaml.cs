using ProjectLifeSaver.Models;
using ProjectLifeSaver.Pages;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UWPHelper.UI;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ProjectLifeSaver
{
    public sealed partial class MainPage : Page
    {
        public static readonly DependencyProperty AIOverlayVisibilityProperty = DependencyProperty.Register(nameof(AIOverlayVisibility), typeof(Visibility), typeof(MainPage), new PropertyMetadata(Visibility.Collapsed, OnAIOverlayVisibilityPropertyChanged));
        public static readonly DependencyProperty PulseProperty = DependencyProperty.Register(nameof(Pulse), typeof(float), typeof(MainPage), new PropertyMetadata(0f));
        public static readonly DependencyProperty PulseConvertedProperty = DependencyProperty.Register(nameof(PulseConverted), typeof(int), typeof(MainPage), new PropertyMetadata(120));
        public static readonly DependencyProperty TemperatureProperty = DependencyProperty.Register(nameof(Temperature), typeof(float), typeof(MainPage), new PropertyMetadata(34f));
        public static readonly DependencyProperty PulseColorProperty = DependencyProperty.Register(nameof(PulseDangerZone), typeof(SolidColorBrush), typeof(MainPage), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public float Pulse
        {
            get { return (float)GetValue(PulseProperty); }
            set { SetValue(PulseProperty, value); }
        }
        public int PulseConverted
        {
            get { return (int)GetValue(PulseConvertedProperty); }
            set { SetValue(PulseConvertedProperty, (int)value); }
        }
        public float Temperature
        {
            get { return (float)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }

        public SolidColorBrush PulseDangerZone
        {
            get { return (SolidColorBrush)GetValue(PulseColorProperty); }
            set { SetValue(PulseColorProperty, value); }
        }

        public static MainPage Current { get; private set; }

        private readonly ApiAiHelper apiAiHelper = new ApiAiHelper();

        public MediaElement Element => _element;

        public ObservableCollection<MessageData> AiMessages { get; }
        private Visibility AIOverlayVisibility
        {
            get { return (Visibility)GetValue(AIOverlayVisibilityProperty); }
            set { SetValue(AIOverlayVisibilityProperty, value); }
        }

        private bool tapped = false;

        public MainPage()
        {
            Current     = this;
            AiMessages  = new ObservableCollection<MessageData>();
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            InitializeComponent();
        }

        public Task GetHelp()
        {
            AIOverlayVisibility = Visibility.Visible;
            TB_Message.Focus(FocusState.Programmatic);

            return apiAiHelper.InitResponse();
        }
        
        private async void SendMessage(object sender, object e)
        {
            if (!tapped)
            {
                tapped = true;

                await apiAiHelper.GetResponse();

                TB_Message.Text = "";
                TB_Message.Focus(FocusState.Programmatic);

                LV_Messages.ScrollIntoView(LV_Messages.Items[LV_Messages.Items.Count - 1]);

                tapped = false;
            }
            
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

                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                await App.Current.SetDefaultBarsColors();
            }
        }
        
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (AIOverlayVisibility == Visibility.Visible)
            {
                e.Handled = true;
                AIOverlayVisibility = Visibility.Collapsed;
            }

        }
    }
}
