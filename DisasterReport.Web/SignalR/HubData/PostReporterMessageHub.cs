using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisasterReport.Web.SignalR.HubData
{
    public class PostReporterMessageHub
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 发送的时间
        /// </summary>
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// 消息状态标志位 1 普通 2重要 3紧急
        /// </summary>
        public virtual int Flag { get; set; }
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


        public virtual String Type { get; set; }
    }
}