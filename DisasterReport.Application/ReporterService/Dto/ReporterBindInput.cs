using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.ReporterService.Dto
{
    public class ReporterBindInput
    {
        public virtual Guid ReporterId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string UserCode { get; set; }
    }
}
