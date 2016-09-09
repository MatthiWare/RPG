using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatthiWare.Networking.Packets
{
    public class MessagePacket : IPacket
    {
        public const byte ID = 0x00;

        public byte Id {get { return ID; } }

        public int From;
        public String Message;

        public MessagePacket()
        { }

        public MessagePacket(int fromId, String msg)
        {
            From = fromId;
            Message = msg;
        }

        public void ReadPacket(UdpReader reader)
        {
            From = reader.ReadInt32();
            Message = reader.ReadString();
        }

        public void WritePacket(UdpWriter writer)
        {
            writer.WriteUInt8(ID);
            writer.WriteInt32(From);
            writer.WriteString(Message);
        }
    }
}
