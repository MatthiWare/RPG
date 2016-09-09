using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDPClient
{
    public class User
    {
        public int id;
        public String username;

        public User(int id, String username)
        {
            this.id = id;
            this.username = username;
        }
    }
}
