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
    public class MessageGroupTb: Entity<Guid>
    {
        /// <summary>
        /// 消息组名称
        /// </summary>
        public virtual string GroupName { get; set; }
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
        ///// <summary>
        ///// 组创建者
        ///// </summary>
        //public virtual ReporterInfoTb GroupOwner { get; set; }
        ///// <summary>
        ///// 管理员组
        ///// </summary>
        //public virtual ICollection<ReporterInfoTb> GroupAdmins { get; set; }
        ///// <summary>
        ///// 消息组成员
        ///// </summary>
        //public virtual ICollection<ReporterInfoTb> GroupMembers { get; set; }
    }
}
