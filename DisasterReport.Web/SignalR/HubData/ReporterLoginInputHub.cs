using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisasterReport.Web.SignalR.HubData
{
    public class ReporterLoginInputHub
    {
        public virtual Guid ReporterId { get; set; }
        public virtual string Name { get; set; }
        public virtual string HubId { get; set; }
        public virtual string Type { get; set; }
    }
}