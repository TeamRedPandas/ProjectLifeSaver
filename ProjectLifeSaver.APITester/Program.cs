using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProjectLifeSaver.APITester
{
    public static class Program
    {
        private const string RECEIVING_STOP_MESSAGE = "678999dd-f661-4f5f-afa3-e0c4a9f7fe16_d9ddf6f2-e57f-4a5d-87ef-30fb8345e499";

        //private static IPAddress remoteIP;
        private static int localPort = 15258;

        public static void Main(string[] args)
        {
            Console.Title = "ProjectLifeSaver.APITester";

            // do
            // {
            //     Console.Write("Enter remote IP address to listen for: ");
            // } while (!IPAddress.TryParse(Console.ReadLine(), out remoteIP));

            // do
            // {
            //     Console.Write("Enter local port to listen on: ");
            // } while (!int.TryParse(Console.ReadLine(), out localPort));

            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Bind(new IPEndPoint(IPAddress.Any, localPort));
                    socket.Listen(1);

                    Socket connection = socket.Accept();

                    while (true)
                    {
                        byte[] data             = new byte[800];
                        int receivedDataLength  = connection.Receive(data);
                        string receivedString   = Encoding.UTF8.GetString(data, 0, receivedDataLength);

                        if (receivedString == RECEIVING_STOP_MESSAGE)
                        {
                            Console.WriteLine("Closing the connection . . .");
                            break;
                        }

                        Console.WriteLine(receivedString);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nSomething happened ( ͡° ͜ʖ ͡°)\n" + exception);
            }

            Console.ReadLine();
        }
    }
}