using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ChatCore
{
    public class Client
    {
        public TcpClient tcpClient;
        public string Name;
        public string Psw;
        public bool HasAcccount { get {  return Server.accounts.Contains(Name); } }
        public bool IsLogin { get { return Server.accounts.Contains(Name); } }
        const string defaultName = "unknown";
        public Client(string name, TcpClient client)
        {
            this.Name = name;
            this.tcpClient = client;
        }
    }
}
