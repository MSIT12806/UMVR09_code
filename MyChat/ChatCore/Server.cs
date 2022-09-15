using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatCore
{
    public class Server
    {
        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        private HashSet<string> accounts = new HashSet<string>();
        TcpListener listener;
        public Server()
        {
            accounts.Add("ROBIN");
        }
        public void Start(int port)
        {
            Console.WriteLine("====================================");
            listener = new TcpListener(IPAddress.Any, port);
            new Thread(HandleMessages).Start();

            try
            {
                Console.WriteLine("Server start at port {0}", port);
                listener.Start();
                Console.WriteLine("Server is ready.");


                //Console.WriteLine("Waiting for a connection... ");
                TcpClient client = listener.AcceptTcpClient();//此段程式碼會停駐，直到有人連進來。
                //new Thread(AcceptConnectAndLogin).Start();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                listener.Stop();
                Console.WriteLine("Server shutdown");
            }
        }

        private void AcceptConnectAndLogin(TcpClient client)
        {
            while (true)
            {

                string ip = client.Client.RemoteEndPoint.ToString();
                Console.WriteLine("Client has connected from {0}", ip);

                lock (clients)
                {
                    clients.Add(client.Client.RemoteEndPoint.ToString(), client);
                    Login(client);

                }
            }
        }

        private void Login(TcpClient client)
        {
            Send($"?", client);

        }

        public Dictionary<string, TcpClient> GetClients()
        {
            return new Dictionary<string, TcpClient>(clients);
        }
        void HandleMessages()
        {
            while (true)
            {
                lock (clients)
                {
                    foreach (var client in clients.Values)
                    {
                        try
                        {
                            if (client.Available > 0)
                            {
                                Receive(client);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: {0}", e);
                        }
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

            var request = System.Text.Encoding.ASCII.GetString(buffer).Substring(0, bytesRead);
            Console.WriteLine(client.Client.RemoteEndPoint.ToString() + ": " + request);
        }

        void Send(string msg, TcpClient client)
        {
            byte[] requestBuffer = null;

            requestBuffer = System.Text.Encoding.UTF8.GetBytes(msg);
            //else if...
            client.GetStream().Write(requestBuffer, 0, requestBuffer.Length);
        }
    }
}
