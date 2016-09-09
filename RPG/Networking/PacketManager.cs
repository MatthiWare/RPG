using MatthiWare.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MatthiWare.Networking
{
    public class PacketManager
    {

        private static Dictionary<byte, Type> packetTypes;

        static PacketManager()
        {
            packetTypes = new Dictionary<byte, Type>(byte.MaxValue);

            AddPacket(MessagePacket.ID, typeof(MessagePacket));
            AddPacket(LoginPacket.ID, typeof(LoginPacket));
        }

        public static IPacket ReadPacket(UdpReader reader)
        {
            byte id = reader.ReadUInt8();

            Type packetType;
            if (!packetTypes.TryGetValue(id, out packetType))
                throw new InvalidOperationException("Invalid packet ID: " + id);

            IPacket packet = PacketFromType(packetType);
            packet.ReadPacket(reader);
            return packet;
        }

        public static IPacket PacketFromType(Type type)
        {
            if (!typeof(IPacket).IsAssignableFrom(type))
                throw new InvalidCastException("Type must inherit from IPacket.");

            IPacket instance = (IPacket)Activator.CreateInstance(type);
            return instance;
        }

        public static void AddPacket(byte id, Type type)
        {
            packetTypes.Add(id, type);
        }


    }
}
