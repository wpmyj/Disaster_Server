using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisasterReport.Web.SignalR
{
    public class CallReporterDealDisasterHub
    {
        public virtual Guid Id { get; set; }
        public virtual string Remark { get; set; }
        public virtual string Address { get; set; }
        public virtual string DisasterType { get; set; }
    }
}