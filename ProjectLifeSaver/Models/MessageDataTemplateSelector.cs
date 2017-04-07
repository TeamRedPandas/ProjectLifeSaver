using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectLifeSaver.Models
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Assistant { get; set; }
        public DataTemplate Me { get; set; }

        protected override DataTemplate SelectTemplateCore(object item,
                                                       DependencyObject container)
        {
            if (((MessageData)item).Sender == "assistant")
                return Assistant;

            if (((MessageData)item).Sender == "me")
                return Me;

            return base.SelectTemplateCore(item, container);
        }
    }
}
