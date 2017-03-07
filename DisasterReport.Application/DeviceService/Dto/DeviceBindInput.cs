using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DeviceService.Dto
{
    public class DeviceBindInput
    {
        public virtual Guid ReporterId { get; set; }
        public virtual int DeviceCode { get; set; }
    }
}
