using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UWPHelper.Utilities;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace ProjectLifeSaver
{
    public static class RemoteDataGetter
    {
        private const string RECEIVING_STOP_MESSAGE = "678999dd-f661-4f5f-afa3-e0c4a9f7fe16_d9ddf6f2-e57f-4a5d-87ef-30fb8345e499";
        private static int localPort = 15258;
        private static bool state = false;

        public static float CurrentPulse;
        private static float K = 200 / (1020 - 1002);
        private static Socket connection = null;
        private static Timer oneTimeTimer;

        public static void GetInfoFromRemoteDevice()
        {
            string Pulse = "";
            string Temp = "";

            oneTimeTimer = new Timer(Shutdown, null, 10000, 1);
           
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Bind(new IPEndPoint(IPAddress.Any, localPort));
                    socket.Listen(2);
                    connection = socket.Accept();

                    while (Pulse == "" || Temp == "")
                    {
                        do
                        {
                            byte[] data = new byte[800];
                            int receivedDataLength = connection.Receive(data);

                            switch (state)
                            {
                                case true:
                                    Pulse = Encoding.UTF8.GetString(data, 0, receivedDataLength);
                                    break;

                                case false:
                                    Temp = Encoding.UTF8.GetString(data, 0, receivedDataLength);
                                    break;
                            }

                            state = !state;
                        } while (state);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("\nSomething happened ( ͡° ͜ʖ ͡°)\n" + exception);
            }

            MainPage.Current.Pulse = float.Parse(Pulse);
            MainPage.Current.PulseConverted = (int)((1020 - CurrentPulse) * K);

            if (MainPage.Current.Pulse < 1005)
                MainPage.Current.PulseDangerZone = new SolidColorBrush(Colors.MistyRose);

            else if (MainPage.Current.Pulse >= 1005 && MainPage.Current.Pulse <= 1007)
                MainPage.Current.PulseDangerZone = new SolidColorBrush(Colors.White);

            else
                MainPage.Current.PulseDangerZone = new SolidColorBrush(Colors.MistyRose);

            MainPage.Current.Temperature = float.Parse(Temp);

            return;
        }

        private static void Shutdown(object state)
        {
            connection?.Shutdown(SocketShutdown.Both);
            connection?.Dispose();

            oneTimeTimer.Dispose();
        }
    }
}
