using ApiAiSDK;
using ProjectLifeSaver.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace ProjectLifeSaver
{
    public class ApiAiHelper
    {
        private static class RequestType
        {
            internal const string COMMAND = "COMMAND";
            internal const string MESSAGE = "MESSAGE";
        }

        private const string API_AI_TOKEN           = "d495e29a70a8476dafc7681fd740e594";

        private const string MSG_INIT               = "COMMAND/initial_message";
        private const string MSG_CALLAMBULANCE      = "COMMAND/call_ambulance_request";
        private const string MSG_CHECKINPUTBYPERSON = "COMMAND/check_input_by_person";
        private const string MSG_HYPERTENSION       = "COMMAND/hypertension";
        private const string MSG_HYPOTENSION        = "COMMAND/hypotension";
        private const string MSG_LOWTEMPERATURE     = "COMMAND/low_temperature";
        private const string MSG_HIGHTEMPERATURE    = "COMMAND/high_temperature";
        private const string MSG_NONE               = "COMMAND/none";
        private const string MSG_GETDATA            = "COMMAND/getData";

        private const string MSG_DIALAMBULANCE      = "COMMAND/dial_ambulance";
        private const string MSG_CANCELDIAL         = "COMMAND/cancel_ambulance";

        private readonly ApiAi apiAi = new ApiAi(new AIConfiguration(API_AI_TOKEN, SupportedLanguage.English));

        /*private bool isInitialized;
        private string lastSent;*/

        private async Task<string> TryGetMessageAsync(string request)
        {
            try
            {
                return (await apiAi.TextRequestAsync(request)).Result.Fulfillment.Speech;
            }
            catch
            {
                return null;
            }
        }

        public async Task AddNewMessage(string Sender, string Message)
        {
            MessageData data = new MessageData()
            {
                Sender = Sender,
                Message = Message,
                Received = DateTime.Now
            };
            MainPage.Current.AiMessages.Add(data);

            if (Sender == MessageData.SENDER_ASSISTANT)
                await SpeechToText.TextToSpeechAsync(MainPage.Current.Element, Message);
        }

        public async Task InitResponse()
        {
            /*lastSent = MSG_INIT;
            isInitialized = true;*/
            var msg = await TryGetMessageAsync(MSG_INIT);

            await AddNewMessage(MessageData.SENDER_ASSISTANT, msg);
        }

        public async Task GetResponse()
        {
            try
            {
                string txt = await SpeechToText.RecordSpeechFromMicrophoneAsync();
                if (txt == "")
                    return;
                await AddNewMessage(MessageData.SENDER_ME, txt);

                string response = await TryGetMessageAsync(txt);

                while (response.Contains("COMMAND"))
                {
                    string[] parts = response.Split('-');

                    try
                    {
                        if (parts[1] != "")
                            await AddNewMessage(MessageData.SENDER_ASSISTANT, parts[1]);
                    }
                    catch
                    {
                        Debug.WriteLine("Only has COMMAND");
                    }

                    if (parts[0] == MSG_GETDATA)
                    {
                        RemoteDataGetter.GetInfoFromRemoteDevice();

                        if (MainPage.Current.PulseDangerZone != new SolidColorBrush(Colors.White))
                        {
                            parts[0] = MainPage.Current.Pulse < 1010 ? MSG_HYPERTENSION : MSG_HYPOTENSION;
                        }

                        else if (MainPage.Current.Temperature < 32 || MainPage.Current.Temperature > 40)
                        {
                            parts[0] = MainPage.Current.Temperature < 32 ? MSG_LOWTEMPERATURE : MSG_HIGHTEMPERATURE;
                        }
                    }

                    if (parts[0] == MSG_CANCELDIAL)
                    {
                        return;
                    }

                    if (parts[0] == MSG_DIALAMBULANCE)
                    {
                        try
                        {
                            await Dial.CallAsync();
                        }
                        catch
                        { }
                        return;
                    }

                    response = await TryGetMessageAsync(parts[0]);
                }

                await AddNewMessage(MessageData.SENDER_ASSISTANT, response);
            }
            catch
            {
                Debug.WriteLine("I DONT HAVE TIME FOR THIS");
            }
            

            /*if (!lastSent.Contains("COMMAND"))
            {
                txt         = await SpeechToText.RecordSpeechFromMicrophoneAsync(); //MainPage.Current.TB_DEBUG_INPUT.Text;
                lastSent    = txt;
                    
                MessageData data = new MessageData()
                {
                    Sender      = MessageData.SENDER_ME,
                    Received    = DateTime.Now,
                    Message     = txt
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
                    /*    return;
                    }

                    if (parts[1] == MSG_DIALAMBULANCE)
                    {
                        await Dial.CallAsync();
                        return;
                    }

                    MessageData data = new MessageData()
                    {
                        Sender      = MessageData.SENDER_ASSISTANT,
                        Received    = DateTime.Now,
                        Message     = parts[2]
                    };

                    MainPage.Current.AiMessages.Add(data);

                    await SpeechToText.TextToSpeechAsync(MainPage.Current.element, parts[2]);

                    if (parts[1] == "getData")
                    {
                        /*TRY TO EVALUATE DATA*/

                    /*    parts[1] = MSG_LOWTEMPERATURE;
                    }

                    result = await TryGetMessageAsync(parts[1]);
                }
                else
                {
                    MessageData data = new MessageData()
                    {
                        Sender      = MessageData.SENDER_ASSISTANT,
                        Received    = DateTime.Now,
                        Message     = result
                    };

                    MainPage.Current.AiMessages.Add(data);
                    await SpeechToText.TextToSpeechAsync(MainPage.Current.element, result);
                }
            }*/
        }
    }
}
