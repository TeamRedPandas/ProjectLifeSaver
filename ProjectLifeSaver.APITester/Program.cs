using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProjectLifeSaver.APITester
{
    public static class Program
    {
        private const string RECEIVING_STOP_MESSAGE = "678999dd-f661-4f5f-afa3-e0c4a9f7fe16_d9ddf6f2-e57f-4a5d-87ef-30fb8345e499";

        private static IPAddress remoteIP;
        private static int localPort;

        public static void Main(string[] args)
        {
            do
            {
                Console.Write("Enter remote IP address to listen for: ");
            } while (!IPAddress.TryParse(Console.ReadLine(), out remoteIP));

            do
            {
                Console.Write("Enter local port to listen on: ");
            } while (!int.TryParse(Console.ReadLine(), out localPort));

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                try
                {
                    EndPoint remoteEndPoint = new IPEndPoint(remoteIP, localPort);
                    socket.Bind(remoteEndPoint);

                    byte[] data = new byte[8];

                    while (true)
                    {
                        int received = socket.ReceiveFrom(data, ref remoteEndPoint);

                        if (Encoding.Unicode.GetString(data, 0, received) == RECEIVING_STOP_MESSAGE)
                        {
                            Console.WriteLine("Closing the connection . . .");
                            break;
                        }

                        Console.WriteLine(Encoding.Unicode.GetString(data, 0, received));
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("\nSomething happened:\n" + exception);
                }
            }

            Console.ReadLine();
        }
    }
}