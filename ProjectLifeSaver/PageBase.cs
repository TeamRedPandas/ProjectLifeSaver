using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ProjectLifeSaver
{
    public abstract class PageBase : Page
    {
        public PageBase()
        {
            //NavigationCacheMode = NavigationCacheMode.Required;

            Transitions = new TransitionCollection()
            {
                new NavigationThemeTransition()
                {
                    DefaultNavigationTransitionInfo = new DrillInNavigationTransitionInfo()
                }
            };
        }
    }
}
