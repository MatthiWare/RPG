using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MatthiWare.Networking
{
    public abstract class UdpBase
    {
        protected UdpClient Client;
        protected UdpWriter Writer;
        protected UdpReader Reader;

        protected UdpBase(IPEndPoint ep)
        {
            Client = new UdpClient(ep);
            Writer = new UdpWriter(Client);
            Reader = new UdpReader();
        }

        protected UdpBase(string hostname, int port)
        {
            Client = new UdpClient(hostname, port);
            Writer = new UdpWriter(Client);
            Reader = new UdpReader();
        }

        public UdpData Receive()
        {
            IPEndPoint ep = null;
            byte[] buffer = Client.Receive(ref ep);
            Reader.SetData(buffer);
            IPacket packet =  PacketManager.ReadPacket(Reader);
            return new UdpData(packet, ep);
        }

        public void Send(IPacket packet, IPEndPoint endpoint)
        {
            packet.WritePacket(Writer);
            Writer.Flush(endpoint);
        }

        public void Send(IPacket packet)
        {
            packet.WritePacket(Writer);
            Writer.Flush();
        }
    }
}
