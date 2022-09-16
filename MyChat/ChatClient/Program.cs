using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClient
{
    public class Client
    {
        private TcpClient me = new TcpClient();
        bool @continue = true;
        public void Connect(string hostIP, int port)
        {
            Console.WriteLine("Connecting to chat server {0}:{1}", hostIP, port);

            me.Connect(hostIP, port);

            if (!me.Connected)
            {
                Console.WriteLine("Can't Connect to chat server");
                return;
            }


            var theThread = new Thread(HandleMessages);
            theThread.Start();

            Console.WriteLine("Connected to chat server");

            while (@continue)
            {
                Console.WriteLine("Say something to server...");
                string msg = Console.ReadLine();
                if (msg != "-e")
                    Send(me, msg);
                else
                    DisConnect();

            }
        }
        public void DisConnect()
        {
            @continue = false;
            if (me.Connected)
            {
                me.Close();
            }
        }
        private void Send(TcpClient client, string msg)
        {
            byte[] requestBuffer = null;

            requestBuffer = System.Text.Encoding.UTF8.GetBytes(msg);
            //else if...


            client.GetStream().Write(requestBuffer, 0, requestBuffer.Length);
        }
        private void TestSend()
        {
            for (int i = 0; i < 10; i++)
            {
                Send(me, "HiHi" + i);
                Thread.Sleep(1000);
            }
        }
        void HandleMessages()
        {
            while (true)
            {
                lock (me)
                {

                    try
                    {
                        if (me.Available > 0)
                        {
                            Receive(me);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}", e);
                    }
                }
            }
        }
        void Receive(TcpClient client)
        {
            var numBytes = client.Available;
            if (numBytes == 0)
            {
                return;
            }

            var stream = client.GetStream();
            var buffer = new byte[numBytes];
            var bytesRead = stream.Read(buffer, 0, numBytes);
            var request = System.Text.Encoding.UTF8.GetString(buffer.Take(bytesRead).ToArray());
            Console.WriteLine( request);
        }
    }
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
