using NotifyPropertyChangedBase;
using System;
using System.Collections.ObjectModel;

namespace ProjectLifeSaver.Models
{
    public sealed class ProfileData : NotifyPropertyChanged
    {
        public static ProfileData Current { get; } = new ProfileData
        {
            Name            = "Gandalf the Grey",
            BirthDate       = new DateTime(17, 2, 16),
            BloodType       = BloodType.O_P,
            AdditionalInfo  = "You shall not pass!!!"
        };

        public string Name
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public int Age
        {
            get
            {
                return (new DateTime(1, 1, 1) + (DateTime.Now - BirthDate)).Year - 1;
            }
        }
        public DateTime BirthDate
        {
            get { return (DateTime)GetValue(); }
            set { SetValue(value); }
        }
        public BloodType BloodType
        {
            get { return (BloodType)GetValue(); }
            set { SetValue(value); }
        }
        public string AdditionalInfo
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public ObservableCollection<Disease> Diseases { get; set; }
        
        public ProfileData()
        {
            RegisterProperty(nameof(Name), typeof(string), null);
            RegisterProperty(nameof(BirthDate), typeof(DateTime), new DateTime(), (sender, e) =>
            {
                OnPropertyChanged(nameof(Age));
            });
            RegisterProperty(nameof(BloodType), typeof(BloodType), BloodType.Unset);
            RegisterProperty(nameof(AdditionalInfo), typeof(string), null);
        }
    }
}
