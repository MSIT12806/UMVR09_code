using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ClientCore;

namespace ChatClient
{
    internal class Program
    {
        static TcpClient client = new TcpClient();
        public static void Main(string[] args)
        {
            const string hostIP = "127.0.0.1";
            const int port = 4099;

            Console.WriteLine("====================================");

            Client client = new Client();
            try
            {
                client.Connect(hostIP, port);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                client.DisConnect();
                Console.WriteLine("Disconnected");
            }
        }



    }
}
