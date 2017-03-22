using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageGroupService.Dto
{
    public class UpdateMessageGroupIconInput
    {
        public virtual Guid MessageGroupId { get; set; }
        public virtual string Photo { get; set; }
    }
}
