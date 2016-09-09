using MatthiWare.Networking;
using MatthiWare.Networking.Packets;
using System;

namespace UDPServer
{
    public class PacketHandlers
    {
        private static int id = 1;

        public static void HandleLoginPacket(Program p, UdpData data)
        {
            LoginPacket packet = (LoginPacket)data.Packet;

            Client newClient = new Client(id, data.RemoteEP, packet.Username);
            p.clients.Add(newClient);

            packet.LoggedIn = true;
            packet.ClientId = id;
            packet.ServerReply = true;
            id++;

            Console.WriteLine(String.Format("[server] {0} logged in.", packet.Username));

            p.SendPacket(packet, data.RemoteEP);

                packet.ServerReply = false;

            p.SendPacketToAllExcluding(packet, data.RemoteEP);

            p.clients.FindAll(c => newClient.id != c.id).ForEach(c => {
                LoginPacket login = new LoginPacket(c.username);
                login.ClientId = c.id;
                login.LoggedIn = true;

                p.SendPacket(login, data.RemoteEP);
            });
        }

        public static void HandleMessagePacket(Program p, UdpData data)
        {
            MessagePacket packet = (MessagePacket)data.Packet;

            Client c = p.clients.Find(x => x.ep.Equals(data.RemoteEP));
           
            Console.WriteLine("[" + c.username + "]: " +  packet.Message);

            MessagePacket reply = new MessagePacket(c.id, packet.Message);
            p.SendPacketToAll(reply);
        }
    }
}
