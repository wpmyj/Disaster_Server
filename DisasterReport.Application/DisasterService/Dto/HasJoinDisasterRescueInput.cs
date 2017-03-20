using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class HasJoinDisasterRescueInput
    {
        public virtual Guid DisasterId { get; set; }
        public virtual Guid ReporterId { get; set; }
    }
}
