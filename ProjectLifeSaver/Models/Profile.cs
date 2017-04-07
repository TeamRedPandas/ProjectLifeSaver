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
        public string Name { get; set; }
        public byte Age { get; set; }
        public BloodType Blood { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Disease> Diseases { get; }

        public Profile(string Name, byte Age, BloodType Blood, string Description)
        {
            this.Name = Name;
            this.Age = Age;
            this.Blood = Blood;
            this.Description = Description;
        }

        public void AddDisease(string Name, string Description) => Diseases.Add(new Disease(Name, Description));
    }
}
