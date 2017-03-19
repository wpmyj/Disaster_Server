using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class DisasterKindUpdateInput
    {
        public virtual Guid Id { get; set; }
        public virtual string Photo { get; set; }
    }
}
