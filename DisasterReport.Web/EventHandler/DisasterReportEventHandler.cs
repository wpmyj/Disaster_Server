using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using DisasterReport.DisasterService.Event;
using DisasterReport.Web.SignalR;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DisasterReport.Web.EventHandler
{
    public class DisasterReportEventHandler : IEventHandler<DisasterReportEventData>,
        IEventHandler<ResponseDisasterEventData>,
        ITransientDependency
    {
        public void HandleEvent(DisasterReportEventData eventData)
        {
            var msgHub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            msgHub.Clients.All.postMessage(new SignalrMessageBody() { Type = "disasterReport", Content = eventData });
        }

        public void HandleEvent(ResponseDisasterEventData eventData)
        {
            var msgHub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            msgHub.Clients.All.reponseDisaster(eventData);
        }
    }
}