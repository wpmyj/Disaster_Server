using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisasterReport.Web.SignalR.HubData
{
    public class ResponseDisasterInputHub
    {
        public virtual Guid ReporterId { get; set; }
        public virtual Guid DisasterId { get; set; }
    }
}