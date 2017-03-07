using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_CommunityCodeTb")]
    public class CommunityCodeTb:Entity<Int64>
    {
        /// <summary>
        /// 社区名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 父节点Id
        /// </summary>
        public virtual Int64 Pid { get; set; }
        /// <summary>
        /// 父节点街道名称
        /// </summary>
        public virtual string Pname { get; set; }
    }
}
