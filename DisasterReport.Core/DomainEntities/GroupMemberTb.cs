using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_GroupMemberTb")]
    public class GroupMemberTb : Entity<Guid>
    {
        /// <summary>
        /// 关联的群
        /// </summary>
        public virtual MessageGroupTb MessageGroup { get; set; }
        /// <summary>
        /// 上报人员
        /// </summary>
        public virtual ReporterInfoTb Reporter { get; set; }
        /// <summary>
        /// 群主 管理员 成员
        /// </summary>
        public virtual int Type { get; set; }
    }
}
