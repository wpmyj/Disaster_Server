using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_MessageGroupTb")]
    public class MessageGroupTb : Entity<Guid>
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
        /// 消息组的头像
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 消息组标签-区分灾情类型
        /// </summary>
        public virtual string Lable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 消息组总人数
        /// </summary>
        public virtual int GroupTotalNum { get; set; }
        /// <summary>
        /// 1）救援队 2）个人团队  两类节点
        /// </summary>
        public virtual int type { get; set; }
        /// <summary>
        /// 关联的Reporter
        /// </summary>
        public virtual ICollection<ReporterInfoTb> Reporter { get; set; }
        /// <summary>
        /// 关联的灾情
        /// </summary>
        public virtual DisasterInfoTb Disaster { get; set; }
    }
}
