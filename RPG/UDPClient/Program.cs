using MatthiWare.Networking;
using MatthiWare.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDPClient
{
    public class Program
    {
        public int clientID;
        public String username;
        private UdpUser client;
        public List<User> users = new List<User>();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        public void Run()
        {
            Console.WriteLine("Establishing connection..");
            client = new UdpUser("127.0.0.1", 43594);
            Console.WriteLine("Connected to server!");

            client.PacketReceived += Client_PacketReceived;

            Console.Write("Username: ");
            username = Console.ReadLine();

            SendPacket(new LoginPacket(username));

            Console.WriteLine("Write 'q' to exit.");

            String input;
            do
            {
                input = Console.ReadLine();
                MessagePacket packet = new MessagePacket(clientID, input);
                SendPacket(packet);
            } while (input != "q");
        }

        private void Client_PacketReceived(object sender, PacketReceivedEventArgs e)
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
                    Console.WriteLine("No packethandler for packet " + e.Data.Packet.Id);
                    break;
            }
        }

        public void SendPacket(IPacket packet)
        {
            client.Send(packet);
        }
    }
}
