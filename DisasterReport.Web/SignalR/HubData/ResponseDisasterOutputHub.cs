using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisasterReport.Web.SignalR.HubData
{
    public class ResponseDisasterOutputHub
    {
        public virtual string Name { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string DisasterKindName { get; set; }
        public virtual string Type { get; set; }
    }
}