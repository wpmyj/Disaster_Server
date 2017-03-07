using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_GroupAdminTb")]
    public class GroupAdminTb: Entity<Guid>
    {
        public virtual MessageGroupTb MessageGroup { get; set; }

        public virtual ICollection<ReporterInfoTb> GroupAdmins { get; set; }
    }
}
