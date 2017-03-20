using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Event
{
    public class ResponseDisasterEventData : EventData
    {
        public virtual Guid ReporterId { get; set; }
        public virtual string Name { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string DisasterKindName { get; set; }
        public virtual string Type { get; set; }
    }
}
