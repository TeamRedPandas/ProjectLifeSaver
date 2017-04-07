using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLifeSaver.Models
{
    public sealed class Disease
    {
        public string Name { get; set; }
        public string Desctiption { get; set; }

        public Disease(string Name, string Description)
        {
            this.Name = Name;
            this.Desctiption = Desctiption;
        }
    }
}
