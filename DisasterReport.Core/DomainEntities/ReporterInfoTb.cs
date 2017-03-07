using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("dr_ReporterInfoTb")] //上报人员信息表
    public class ReporterInfoTb : Entity<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual String Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual String Phone { get; set; }
        /// <summary>
        /// 所属区域编码
        /// </summary>
        public virtual String AreaCode { get; set; }
        /// <summary>
        /// 所在地址
        /// </summary>
        public virtual String Address { get; set; }
        /// <summary>
        /// 用户账号关联
        /// </summary>
        public virtual UserTb User { get; set; }
        /// <summary>
        /// 关联的消息组-owner
        /// </summary>
        public virtual ICollection<GroupOwnerTb> GroupOwner { get; set; }
        /// <summary>
        /// 关联的消息组-管理员
        /// </summary>
        public virtual ICollection<GroupAdminTb> GroupAdmin { get; set; }
        /// <summary>
        /// 关联的消息组-成员
        /// </summary>
        public virtual ICollection<GroupMemberTb> GroupMember { get; set; }
    }
}
