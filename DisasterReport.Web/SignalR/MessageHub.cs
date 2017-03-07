using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace DisasterReport.Web.SignalR
{
    [HubName("messageHub")]
    public class MessageHub : Hub, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public MessageHub()
        {
            Logger = NullLogger.Instance;
        }
        public override async Task OnConnected()
        {
            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);
        }
    }
}