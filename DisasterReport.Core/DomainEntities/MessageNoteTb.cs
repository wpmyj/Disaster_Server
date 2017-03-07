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
        /// 接收者
        /// </summary>
        public virtual ReporterInfoTb ToReporter { get; set; }
        /// <summary>
        /// 发送的消息
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 发送的时间
        /// </summary>
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// 消息状态标志位（最新 已读 未读...）
        /// </summary>
        public virtual int Flag { get; set; }
        /// <summary>
        /// 消息类型（私信 群消息）
        /// </summary>
        public virtual int Type { get; set; }
    }
}
