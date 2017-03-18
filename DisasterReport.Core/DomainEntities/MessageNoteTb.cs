using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_MessageNoteTb")] // 消息通知记录表
    public class MessageNoteTb : Entity<Guid>
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public virtual ReporterInfoTb FromReporter { get; set; }
        /// <summary>
        /// 接收者 null为默认群发 有则为@
        /// </summary>
        public virtual ICollection<ReporterInfoTb> ToReporter { get; set; }
        /// <summary>
        /// 发送的消息
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 发送的时间
        /// </summary>
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// 消息状态标志位 1 普通 2重要 3紧急
        /// </summary>
        public virtual int Flag { get; set; }
        /// <summary>
        /// 消息类型（1群消息 2@发送）
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
