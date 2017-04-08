using NotifyPropertyChangedBase;

namespace ProjectLifeSaver.Models
{
    public sealed class Number : NotifyPropertyChanged
    {
        public enum Types
        {
            Unset,
            Police,
            Ambulance,
            FireDept,
            Sos
        }

        public string DialNumber
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public Types Type
        {
            get { return (Types)GetValue(); }
            set { SetValue(value); }
        }

        public Number()
        {
            RegisterProperty(nameof(DialNumber), typeof(string), null);
            RegisterProperty(nameof(Type), typeof(Types), Types.Unset);
        }
    }
}
