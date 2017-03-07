using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Event
{
    public class DisasterReportEventData : EventData
    {
        /// <summary>
        /// 灾情信息Id
        /// </summary>
        public virtual Guid DisasterInfoId { get; set; }
    }
}
