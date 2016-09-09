using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatthiWare.Networking
{
    public class PacketReceivedEventArgs : EventArgs
    {
        public UdpData Data { get; private set; }

        public PacketReceivedEventArgs(UdpData data)
        {
            Data = data;
        }
    }
}
