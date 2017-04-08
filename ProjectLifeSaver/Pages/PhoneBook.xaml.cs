using ProjectLifeSaver.Models;
using System.Collections.ObjectModel;
using static ProjectLifeSaver.Models.Number;

namespace ProjectLifeSaver.Pages
{
    public sealed partial class PhoneBook : PageBase
    {
        private ObservableCollection<Number> CzechRepublic { get; } = new ObservableCollection<Number>
        {
            new Number
            {
                DialNumber  = "158",
                Type        = Types.Police
            },
            new Number
            {
                DialNumber  = "155",
                Type        = Types.Ambulance
            },
            new Number
            {
                DialNumber  = "150",
                Type        = Types.FireDept
            },
            new Number
            {
                DialNumber  = "112",
                Type        = Types.Sos
            }
        };
        private ObservableCollection<Number> Germany { get; } = new ObservableCollection<Number>
        {
            new Number
            {
                DialNumber  = "110",
                Type        = Types.Police
            },
            new Number
            {
                DialNumber  = "112",
                Type        = Types.Ambulance
            },
            new Number
            {
                DialNumber  = "112",
                Type        = Types.FireDept
            },
            new Number
            {
                DialNumber  = "112",
                Type        = Types.Sos
            }
        };
        private ObservableCollection<Number> Poland { get; } = new ObservableCollection<Number>
        {
            new Number
            {
                DialNumber  = "997",
                Type        = Types.Police
            },
            new Number
            {
                DialNumber  = "999",
                Type        = Types.Ambulance
            },
            new Number
            {
                DialNumber  = "998",
                Type        = Types.FireDept
            },
            new Number
            {
                DialNumber  = "112",
                Type        = Types.Sos
            }
        };
        private ObservableCollection<Number> Austria { get; } = new ObservableCollection<Number>
        {
            new Number
            {
                DialNumber  = "133",
                Type        = Types.Police
            },
            new Number
            {
                DialNumber  = "144",
                Type        = Types.Ambulance
            },
            new Number
            {
                DialNumber  = "122",
                Type        = Types.FireDept
            },
            new Number
            {
                DialNumber  = "112",
                Type        = Types.Sos
            }
        };
        private ObservableCollection<Number> Slovakia { get; } = new ObservableCollection<Number>
        {
            new Number
            {
                DialNumber  = "158",
                Type        = Types.Police
            },
            new Number
            {
                DialNumber  = "155",
                Type        = Types.Ambulance
            },
            new Number
            {
                DialNumber  = "150",
                Type        = Types.FireDept
            },
            new Number
            {
                DialNumber  = "112",
                Type        = Types.Sos
            }
        };

        public PhoneBook()
        {
            Name    = "Phone book";
            InitializeComponent();
        }
    }
}
