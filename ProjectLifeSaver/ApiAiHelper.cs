using ApiAiSDK;
using ProjectLifeSaver.Models;
using System;
using System.Threading.Tasks;

namespace ProjectLifeSaver
{
    public class ApiAiHelper
    {
        private const string API_AI_TOKEN           = "d495e29a70a8476dafc7681fd740e594";

        private const string MSG_INIT               = "initial_message";
        private const string MSG_CALLAMBULANCE      = "call_ambulance_request";
        private const string MSG_CHECKINPUTBYPERSON = "check_input_by_person";
        private const string MSG_HYPERTENSION       = "hypertension";
        private const string MSG_HYPOTENSION        = "hypotension";
        private const string MSG_LOWTEMPERATURE     = "low_temperature";
        private const string MSG_HIGHTEMPERATURE    = "high_temperature";
        private const string MSG_NONE               = "none";
        private const string MSG_GETDATA            = "getData";

        private const string MSG_DIALAMBULANCE      = "dial_ambulance";
        private const string MSG_CANCELDIAL         = "cancel_ambulance";

        private bool isInitialized;
        private string lastSent;
        private ApiAi apiAi;

        public ApiAiHelper()
        {
            AIConfiguration config = new AIConfiguration(API_AI_TOKEN, SupportedLanguage.English);
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
            if (!isInitialized)
            {
                lastSent = MSG_INIT;
                isInitialized = true;
                var msg = await TryGetMessageAsync(MSG_INIT);
                MessageData data = new MessageData()
                {
                    Sender = MessageData.SENDER_ASSISTANT,
                    Received = DateTime.Now,
                    Message = msg
                };

                MainPage.Current.AiMessages.Add(data);

                await SpeechToText.TextToSpeechAsync(MainPage.Current.element, msg);
            }

            else
            {
                string txt = "";
                string result = "";

                if (!lastSent.Contains("COMMAND"))
                {
                    txt = await SpeechToText.RecordSpeechFromMicrophoneAsync(); //MainPage.Current.TB_DEBUG_INPUT.Text;
                    lastSent = txt;
                    
                    MessageData data = new MessageData()
                    {
                        Sender = MessageData.SENDER_ME,
                        Received = DateTime.Now,
                        Message = txt
                    };

                    MainPage.Current.AiMessages.Add(data);

                    result = await TryGetMessageAsync(txt);
                }

                while (true)
                {
                    if (result.Contains("COMMAND"))
                    {
                        string[] parts = result.Split('-');

                        if (parts[1] == MSG_CANCELDIAL)
                        {
                            /*RESET TO DEFAULT*/
                            return;
                        }

                        if (parts[1] == MSG_DIALAMBULANCE)
                        {
                            await Dial.CallAsync();
                            return;
                        }

                        MessageData data = new MessageData()
                        {
                            Sender = MessageData.SENDER_ASSISTANT,
                            Received = DateTime.Now,
                            Message = parts[2]
                        };

                        MainPage.Current.AiMessages.Add(data);

                        await SpeechToText.TextToSpeechAsync(MainPage.Current.element, parts[2]);

                        if (parts[1] == "getData")
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
                            Sender = MessageData.SENDER_ASSISTANT,
                            Received = DateTime.Now,
                            Message = result
                        };

                        MainPage.Current.AiMessages.Add(data);
                        await SpeechToText.TextToSpeechAsync(MainPage.Current.element, result);

                        return;
                    }
                }
            }
        }
    }
}
