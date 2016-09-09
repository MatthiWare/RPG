using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UDPServer
{
    public class Client
    {
        public int id;
        public IPEndPoint ep;
        public String username;

        public Client(int id, IPEndPoint ep, String username)
        {
            this.id = id;
            this.username = username;
            this.ep = ep;
        }
    }
}
