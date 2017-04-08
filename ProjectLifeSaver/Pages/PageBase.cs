using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ProjectLifeSaver.Pages
{
    public abstract class PageBase : Page
    {
        public PageBase()
        {
            //NavigationCacheMode = NavigationCacheMode.Required;

            Transitions = new TransitionCollection()
            {
                new EntranceThemeTransition()
                {
                    FromHorizontalOffset = 0,
                    FromVerticalOffset   = 0
                }
            };
        }
    }
}
