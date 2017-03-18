using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageGroupService.Dto
{
    public class MessageGroupAddInput
    {
        /// <summary>
        /// 消息组名称
        /// </summary>
        public virtual string GroupName { get; set; }
        /// <summary>
        /// 消息组介绍
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 1）救援队 2）个人团队  两类节点
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 负责人Id
        /// </summary>
        public virtual Guid ReporterId { get; set; }
    }
}
