using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace ClientCore
{
    public class Client
    {
        private TcpClient me = new TcpClient();
        bool @continue = true;
        List<string> _message = new List<string>();
        public bool Connected { get { return me.Connected; } }
        public List<string> ShowMessage()
        {
            var m = new List<string>(_message);
            _message.Clear();
            return m;
        }
        public void Connect(string hostIP, int port)
        {
            Console.WriteLine("Connecting to chat server {0}:{1}", hostIP, port);

            me.Connect(hostIP, port);

            if (!me.Connected)
            {
                throw new Exception("Can't Connect to chat server");
            }


            var theThread = new Thread(HandleMessages);
            theThread.Start();

            Console.WriteLine("Connected to chat server");

            while (@continue)
            {
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
        public void Send(string msg)
        {
            byte[] requestBuffer = null;

            requestBuffer = System.Text.Encoding.UTF8.GetBytes(msg);
            //else if...


            me.GetStream().Write(requestBuffer, 0, requestBuffer.Length);
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
            _message.Add(request);
        }
    }

}
