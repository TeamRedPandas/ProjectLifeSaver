using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace ProjectLifeSaver.Models
{
    class SpeechToText
    {
        private static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public static async Task<string> RecordSpeechFromMicrophoneAsync()
        {
            string recognizedText = string.Empty;

            using (SpeechRecognizer recognizer =
              new Windows.Media.SpeechRecognition.SpeechRecognizer())
            {
                await recognizer.CompileConstraintsAsync();

                SpeechRecognitionResult result = await recognizer.RecognizeAsync();

                if (result.Status == SpeechRecognitionResultStatus.Success)
                {
                    recognizedText = result.Text;
                }
            }
            return recognizedText;
        }

        public static async Task TextToSpeechAsync(MediaElement e, string text)
        {
            SpeechSynthesisStream synthesisStream = await synthesizer.SynthesizeTextToStreamAsync(text);
            e.SetSource(synthesisStream, synthesisStream.ContentType);
            e.Play();
        }
    }
}
