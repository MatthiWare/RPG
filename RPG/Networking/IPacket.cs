using System.Net;

namespace MatthiWare.Networking
{
    public interface IPacket
    {
        byte Id { get; }
        void ReadPacket(UdpReader reader);
        void WritePacket(UdpWriter writer);
    }
}
