using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.ReporterService.Dto
{
    public class UpdateReporterLastPosInput
    {
        public virtual Guid Id { get; set; }
        public virtual double Lng { get; set; }
        public virtual double Lat { get; set; }
        public virtual string Address { get; set; }
    }
}
