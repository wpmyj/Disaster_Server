using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisasterReport.Web.SignalR.HubData
{
    public class ChatMessageInputHub
    {
        public virtual Guid FromerId { get; set; }
        public virtual Guid ToerId { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual String Content { get; set; }
    }
}