using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatthiWare.Networking.Packets
{
    public class LoginPacket : IPacket
    {
        public const byte ID = 1;
        public byte Id { get { return ID; } }

        public bool LoggedIn = false;
        public bool ServerReply = false;
        public int ClientId =-1;
        public String Username ="NULL";

        public LoginPacket()
        { }

        public LoginPacket(String username)
        {
            this.Username = username;
        }

        public LoginPacket(bool loggedIn, int id)
        {
            ClientId = id;
            LoggedIn = loggedIn;
        }

        public void ReadPacket(UdpReader reader)
        {
            LoggedIn = reader.ReadBoolean();
            ClientId = reader.ReadInt32();
            Username = reader.ReadString();
        }

        public void WritePacket(UdpWriter writer)
        {
            writer.WriteUInt8(ID);

            writer.WriteBoolean(LoggedIn);
            writer.WriteInt32(ClientId);
            writer.WriteString(Username);
        }
    }
}
