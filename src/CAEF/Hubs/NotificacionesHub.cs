using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

namespace CAEF.Hubs
{
    public class NotificacionesHub : Hub
    {
        public static int ConnectionCount = 0;

        public void Hello()
        {
            Clients.All.hello();
        }

        public override Task OnConnected()
        {
            Interlocked.Increment(ref ConnectionCount);
            Clients.All.reportConnections(ConnectionCount);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Interlocked.Decrement(ref ConnectionCount);
            Clients.All.reportConnections(ConnectionCount);
            return base.OnDisconnected(stopCalled);
        }
    }
}
