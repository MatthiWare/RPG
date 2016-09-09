using MatthiWare.Networking;
using MatthiWare.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UDPServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        public List<Client> clients = new List<Client>();
        private UdpListener listener;

        public void Run()
        {
            Console.WriteLine("Starting UDP Server..");

            listener = new UdpListener(43594);
            listener.PacketReceived += Listener_PacketReceived;
            listener.Start();

            Console.WriteLine("Server listening on port 43594");

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();

            listener.Stop();
        }

        private void Listener_PacketReceived(object sender, PacketReceivedEventArgs e)
        {
            switch (e.Data.Packet.Id)
            {
                case MessagePacket.ID:
                    PacketHandlers.HandleMessagePacket(this, e.Data);
                    break;

                case LoginPacket.ID:
                    PacketHandlers.HandleLoginPacket(this, e.Data);
                    break;

                default:
                    Console.WriteLine("No packethandler available for packet " + e.Data.Packet.Id);
                    break;
            }
        }

        public void SendPacket(IPacket packet, IPEndPoint ep)
        {
            listener.Send(packet, ep);
        }

        public void SendPacketToAll(IPacket packet)
        {
            clients.ForEach(c => listener.Send(packet, c.ep));
        }

        public void SendPacketToAllExcluding(IPacket packet, IPEndPoint ep)
        {
            clients.FindAll(x => !x.ep.Equals(ep)).ForEach(c => listener.Send(packet, c.ep));
        }
    }
}
