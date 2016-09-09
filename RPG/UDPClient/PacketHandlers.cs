using System;
using MatthiWare.Networking;
using MatthiWare.Networking.Packets;


namespace UDPClient
{
    public class PacketHandlers
    {
        public static void HandleLoginPacket(Program p, UdpData data)
        {
            LoginPacket packet = (LoginPacket)data.Packet;

            if (packet.ServerReply)
            {
                p.clientID = packet.ClientId;
                Console.WriteLine(packet.LoggedIn ? "Succesfully logged in!" : "Failed logging in!");
            }
            else
            {
                Console.WriteLine(String.Format("[server]: {0} Logged in", packet.Username));
            }

            User u = new User(packet.ClientId, packet.Username);
            p.users.Add(u);
        }

        public static void HandleMessagePacket(Program p, UdpData data)
        {
            MessagePacket packet = (MessagePacket)data.Packet;
            User u = p.users.Find(x => x.id == packet.From);
            Console.WriteLine(String.Format("[{0}]: {1}", u.username, packet.Message));
        }
    }
}
