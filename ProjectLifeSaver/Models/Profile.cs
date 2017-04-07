using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectLifeSaver.Models.Blood;

namespace ProjectLifeSaver.Models
{
    class Profile
    {
        private static DateTime ZeroDateTime
        {
            get { return new DateTime(1, 1, 1); }
        }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public BloodType Blood { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Disease> Diseases { get; }

        public int Age
        {
            get
            {
                return (Profile.ZeroDateTime + (DateTime.Now - BirthDate)).Year - 1;
            }
        }

        public Profile(string Name, DateTime BirthDate, BloodType Blood, string Description)
        {
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.Blood = Blood;
            this.Description = Description;
        }

        public void AddDisease(string Name, string Description) => Diseases.Add(new Disease(Name, Description));
    }
}
