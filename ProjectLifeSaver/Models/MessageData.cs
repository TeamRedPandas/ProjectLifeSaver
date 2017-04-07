using NotifyPropertyChangedBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLifeSaver.Models
{
    public sealed class MessageData : NotifyPropertyChanged
    {
        public string Message
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public string Sender
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public DateTime Recieved
        {
            get { return (DateTime)GetValue(); }
            set { SetValue(value); }
        }

        public MessageData()
        {
            RegisterProperty(nameof(Message), typeof(string), null);
            RegisterProperty(nameof(Sender), typeof(string), null);
            RegisterProperty(nameof(Recieved), typeof(DateTime), new DateTime());
        }
    }
}
