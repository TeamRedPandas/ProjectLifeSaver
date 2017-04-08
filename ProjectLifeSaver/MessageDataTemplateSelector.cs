using ProjectLifeSaver.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectLifeSaver
{
    public sealed class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Assistant { get; set; }
        public DataTemplate Me { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (((MessageData)item).Sender == MessageData.ASSISTANT_MESSAGE)
            {
                return Assistant;
            }
            else if (((MessageData)item).Sender == MessageData.ME_MESSAGE)
            {
                return Me;
            }

            throw new Exception(@"You're not doing it right ¯\_ツ_/¯");
        }
    }
}
