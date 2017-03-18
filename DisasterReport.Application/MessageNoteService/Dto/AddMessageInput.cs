using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageNoteService.Dto
{
    public class AddMessageInput
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public virtual Guid FromReporterId { get; set; }
        /// <summary>
        /// 发送的消息
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 消息类型（1群发 2@发送）
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public virtual string Topic { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public virtual string Summary { get; set; }
        /// <summary>
        /// 消息title
        /// </summary>
        public virtual string Title { get; set; }
    }
}
