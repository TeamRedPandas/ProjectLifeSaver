using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls;

namespace ProjectLifeSaver.Models
{
    public static class Dial
    {
        public static string DEBUG = "+420604540531";    /*INSERT NUMBER*/
      
        public static async Task CallAsync()
        {
            PhoneCallStore PhoneCallStore = await PhoneCallManager.RequestStoreAsync();
            Guid LineGuid = await PhoneCallStore.GetDefaultLineAsync();

            PhoneLine line = await PhoneLine.FromIdAsync(LineGuid);
            line.Dial(DEBUG, "CALL");
        }
    }
}
