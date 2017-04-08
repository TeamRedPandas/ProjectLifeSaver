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
            BirthDate       = new DateTime(13, 2, 16),
            BloodType       = BloodType.O_P,
            AdditionalInfo  = "You shall not pass!!!",
            Diseases        = new ObservableCollection<Disease>
            {
                new Disease
                {
                    Name        = "Crippling depression",
                    Description = "Severe depression that takes over your whole life making unbearable to carry out basic daily tasks."
                },
                new Disease
                {
                    Name        = "Allergy to necromancy",
                    Description = "Supposed practice of magic involving communication with the deceased for the purpose of divination, imparting the means to foretell future events or discover hidden knowledge, to bring someone back from the dead, or to use the deceased as a weapon, as the term may sometimes be used in a more general sense to refer to black magic or witchcraft."
                }
            }
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
