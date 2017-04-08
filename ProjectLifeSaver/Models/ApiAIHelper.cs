using ApiAiSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls;
using Windows.UI.Xaml;

namespace ProjectLifeSaver.Models
{
    public class ApiAiHelper
    {
        private string ApiAiToken = "d495e29a70a8476dafc7681fd740e594";
        private ApiAi apiAi;

        private const string MSG_INIT = "initial_message";
        private const string MSG_CALLAMBULANCE = "call_ambulance_request";
        private const string MSG_CHECKINPUTBYPERSON = "check_input_by_person";
        private const string MSG_HYPERTENSION = "hypertension";
        private const string MSG_HYPOTENSION = "hypotension";
        private const string MSG_LOWTEMPERATURE = "low_temperature";
        private const string MSG_HIGHTEMPERATURE = "high_temperature";
        private const string MSG_NONE = "none";
        private const string MSG_GETDATA = "getData";

        private const string MSG_DIALAMBULANCE = "dial_ambulance";
        private const string MSG_CANCELDIAL = "cancel_ambulance";

        private string LAST_SENT = "";
        private bool INIT_DONE = false;

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

        public async Task GetResponse()
        {
            if (!INIT_DONE)
            {
                LAST_SENT = MSG_INIT;
                INIT_DONE = true;
                var msg = await TryGetMessageAsync(MSG_INIT);
                MessageData data = new MessageData()
                {
                    Sender = MessageData.ASSISTANT_MESSAGE,
                    Received = DateTime.Now,
                    Message = msg
                };
                MainPage.Current.Log.Add(data);
            }

            else
            {
                string txt = "";
                string result = "";

                if (!LAST_SENT.Contains("COMMAND"))
                {
                    txt = MainPage.Current.TB_DEBUG_INPUT.Text;
                    LAST_SENT = txt;

                    MessageData data = new MessageData()
                    {
                        Sender = MessageData.ME_MESSAGE,
                        Received = DateTime.Now,
                        Message = txt
                    };

                    MainPage.Current.Log.Add(data);

                    result = await TryGetMessageAsync(txt);
                }
                
                do
                {
                    if (result.Contains("COMMAND"))
                    {
                        string[] parts = result.Split('-');

                        if(parts[1] == MSG_CANCELDIAL)
                        {
                            /*RESET TO DEFAULT*/
                            return;
                        }

                        if(parts[1] == MSG_DIALAMBULANCE)
                        {
                            await Dial.CallAsync();
                            return;
                        }

                        MessageData data = new MessageData()
                        {
                            Sender = MessageData.ASSISTANT_MESSAGE,
                            Received = DateTime.Now,
                            Message = parts[2]
                        };

                        MainPage.Current.Log.Add(data);

                        if(parts[1] == "getData")
                        {
                            /*TRY TO EVALUATE DATA*/

                            parts[1] = MSG_LOWTEMPERATURE;
                        }

                        result = await TryGetMessageAsync(parts[1]);
                    }
                    else
                    {

                        MessageData data = new MessageData()
                        {
                            Sender = MessageData.ASSISTANT_MESSAGE,
                            Received = DateTime.Now,
                            Message = result
                        };

                        MainPage.Current.Log.Add(data);

                        return;
                    }

                } while (true);
            }
        }
    }
}
