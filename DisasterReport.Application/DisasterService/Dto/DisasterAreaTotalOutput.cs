using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class DisasterAreaTotalOutput
    {
        public virtual string Name { get; set; }
        public virtual int Count { get; set; }
        public virtual double Lng { get; set; }
        public virtual double Lat { get; set; }
    }
}
