using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace MatthiWare.Networking
{
    public class UdpUser : UdpBase
    {
        private Thread thReceiver;
        private bool Running = false;

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        public UdpUser(string hostname, int port)
            : base(hostname, port)
        {
            Running = true;

            thReceiver = new Thread(new ThreadStart(Receiver));
            thReceiver.Name = "Receiver(" + ((IPEndPoint)Client.Client.LocalEndPoint).Port + ")";
            thReceiver.Priority = ThreadPriority.BelowNormal;
            thReceiver.Start();
        }

        private void Receiver()
        {
            while (Running)
            {
                UdpData data = Receive();
                if (PacketReceived != null)
                    PacketReceived(this, new PacketReceivedEventArgs(data));
            }
        }

        public void Close()
        {
            thReceiver.Abort();
            Running = false;
        }
    }
}
