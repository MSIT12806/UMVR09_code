using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ChatCore
{
    public class Client
    {
        public TcpClient tcpClient;
        public string name;
        public string psw;
        public Client(string name, TcpClient client)
        {
            this.name = name;
            this.tcpClient = client;
        }
    }
}
