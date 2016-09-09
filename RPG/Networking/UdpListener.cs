using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace MatthiWare.Networking
{
    public class UdpListener : UdpBase
    {
        public bool Running { get; private set; }

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        private Thread thListener;

        public UdpListener(int port)
            :base(new System.Net.IPEndPoint(IPAddress.Any, port))
        {
            Running = false;
        }

        public void Start()
        {
            if (Running)
                throw new InvalidOperationException("Listener is already Listening");

            Running = true;

            thListener = new Thread(new ThreadStart(Listen));
            thListener.Name = "UdpListener(" + ((IPEndPoint)Client.Client.LocalEndPoint).Port+")";
            thListener.Priority = ThreadPriority.BelowNormal;
            thListener.Start();
        }

        private void Listen()
        {
            while (Running)
            {
                UdpData data = Receive();
                if (PacketReceived != null)
                    PacketReceived(this, new PacketReceivedEventArgs(data));
            }
        }

        public void Stop()
        {
            Running = false;
            thListener.Abort();
        }
    }
}
