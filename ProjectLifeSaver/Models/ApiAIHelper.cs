using ApiAiSDK;
using System.Threading.Tasks;

namespace ProjectLifeSaver.Models
{
    public class ApiAiHelper
    {
        private string ApiAiToken = "d495e29a70a8476dafc7681fd740e594";
        private ApiAi apiAi;

        private const string MSG_INIT = "initial_message";

        public ApiAiHelper()
        {
            var config = new AIConfiguration(ApiAiToken, SupportedLanguage.English);
            apiAi = new ApiAi(config);
        }

        public async Task<string> TryGetMessageAsync(string msg)
        {
            try
            {
                var response = await apiAi.TextRequestAsync(msg);
                return response.Result.Fulfillment.Speech;
            }
            catch
            {
                return null;
            }
        }

        public async void GetResponse()
        {
            var msg = await TryGetMessageAsync(MSG_INIT);
            

        }
    }
}
