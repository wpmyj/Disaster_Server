using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageGroupService.Dto
{
    public class GroupMemberAddInput
    {
        /// <summary>
        /// 消息组Id
        /// </summary>
        public virtual Guid MessageGroupId { get; set; }
        /// <summary>
        /// 上报人员Id
        /// </summary>
        public virtual List<Guid> ReporterId { get; set; }
    }
}
