using Abp.AutoMapper;
using Abp.Domain.Entities;
using DisasterReport.DomainEntities;
using DisasterReport.ReporterService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageNoteService.Dto
{
    [AutoMap(typeof(MessageNoteTb))]
    public class MessageNoteOutput: Entity<Guid>
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public virtual ReporterOutput FromReporter { get; set; }
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
