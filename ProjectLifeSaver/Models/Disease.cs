using NotifyPropertyChangedBase;

namespace ProjectLifeSaver.Models
{
    public sealed class Disease : NotifyPropertyChanged
    {
        public string Name
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public string Description
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }

        public Disease()
        {
            RegisterProperty(nameof(Name), typeof(string), null);
            RegisterProperty(nameof(Description), typeof(string), null);
        }
    }
}
