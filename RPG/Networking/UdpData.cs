using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MatthiWare.Networking
{
    public class UdpData
    {
        public IPEndPoint RemoteEP { get; private set; }
        public IPacket Packet { get; private set; }

        public UdpData(IPacket packet, IPEndPoint ep)
        {
            Packet = packet;
            RemoteEP = ep;
        }
    }
}
