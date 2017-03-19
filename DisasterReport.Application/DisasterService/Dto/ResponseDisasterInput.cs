using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class ResponseDisasterInput
    {
        public virtual Guid ReporterId { get; set; }
        public virtual Guid DisasterId { get; set; }
    }
}
