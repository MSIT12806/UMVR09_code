using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatCore
{
    public class Server
    {
        private Dictionary<string, Client> clients = new Dictionary<string, Client>();
        public static readonly HashSet<string> accounts = new HashSet<string>();
        TcpListener listener;
        public Server()
        {
            InitAccounts();
        }
        private void InitAccounts()
        {
            accounts.Add("ROBIN");
        }
        public void Start(int port)
        {
            Console.WriteLine("Generate A thread to receive client input.");
            new Thread(HandleMessages).Start();

            try
            {
                Console.WriteLine("Server start at port {0}", port);
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Console.WriteLine("Server is ready.");

                while (true)
                {
                    //Console.WriteLine("Waiting for a connection... ");
                    TcpClient client = listener.AcceptTcpClient();//此段程式碼會停駐，直到有人連進來。

                    string ip = client.Client.RemoteEndPoint.ToString();
                    Console.WriteLine("Client has connected from {0}", ip);

                    var c = new Client("unknown", client);
                    lock (clients)
                    {
                        clients.Add(client.Client.RemoteEndPoint.ToString(), c);
                    }
                    Send("歡迎來到聊天室\n", client);
                    SendHelp(client);
                    Send("請先登入\n", client);
                }
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

        private void SendHelp(TcpClient client)
        {
            string msg =
                "======== 指令表 ========\n" +
                "-h 指令表\n" +
                "-login::[帳號名]登入\n" +
                "-logout 登出\n" +
                "-exit 離開聊天室\n" +
                "-members 顯示目前在線上的成員\n" +
                "[內容] 全頻廣播\n" +
                "-to::[帳號名]::[內容] 私頻\n" +
            "======== End ========\n";
            Send(msg, client);
        }

        private void AcceptConnectAndLogin(TcpClient client)
        {
            while (true)
            {

            }
        }


        public Dictionary<string, Client> GetClients()
        {
            return new Dictionary<string, Client>(clients);
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
                            if (client.tcpClient.Available > 0)
                            {
                                Receive(client.tcpClient);
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
            var request = System.Text.Encoding.UTF8.GetString(buffer.Take(bytesRead).ToArray());
            //......
            if (request.StartsWith("-h", StringComparison.CurrentCultureIgnoreCase))
            {
                SendHelp(client);
            }
            else if (request.StartsWith("-login", StringComparison.CurrentCultureIgnoreCase))
            {
                Login(client, request);
            }
            else if (request.StartsWith("-logout", StringComparison.CurrentCultureIgnoreCase))
            {
                Logout(client);
            }
            else if (request.StartsWith("-exit", StringComparison.CurrentCultureIgnoreCase))
            {
                Exit(client);
            }
            else if (request.StartsWith("-members", StringComparison.CurrentCultureIgnoreCase))
            {
                ShowMembers(client);
            }
            else if (request.StartsWith("--to", StringComparison.CurrentCultureIgnoreCase))
            {
                SendToSomeone(client, request);
            }
            else if (request.StartsWith("--", StringComparison.CurrentCultureIgnoreCase))
            {
                Send("無效的指令。", client);
            }
            else
            {
                Broadcast(request);
            }
        }

        private void Broadcast(string msg)
        {
            lock (clients)
            {
                foreach (var item in clients.Values)
                {
                    Send(msg, item.tcpClient);
                }
            }
        }

        private void SendToSomeone(TcpClient client, string request)
        {
            Send("悄悄話：[" + request + "]", client);
        }

        private void ShowMembers(TcpClient client)
        {
            StringBuilder st = new StringBuilder();
            lock (clients)
            {

                foreach (var item in clients.Values)
                {
                    st.Append(item.Name + "/n");
                }
            }
            Send(st.ToString(), client);
        }

        private void Exit(TcpClient client)
        {
            throw new NotImplementedException();
        }

        private void Logout(TcpClient client)
        {
            throw new NotImplementedException();
        }

        private void Login(TcpClient client, string request)
        {
            string[] command = request.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
            if (command.Length != 2)
            {
                Send("指令錯誤", client);
                return;
            }


            string username = accounts.Contains(command[1]) ? command[1] : "";
            if (username == "")
            {
                Send($"錯誤：帳號不存在。", client);
                return;
            }


            string ip = client.Client.RemoteEndPoint.ToString();

            Client c = null;
            clients.TryGetValue(ip, out c);
            c.Name = username;
            Send($"{username}登入成功", client);
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
