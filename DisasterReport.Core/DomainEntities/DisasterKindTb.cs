using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_DisasterKindTb")] //上报灾情种类表
    public class DisasterKindTb : Entity<Guid>
    {
        /// <summary>
        /// 灾情种类名称
        /// </summary>
        public virtual String Name { get; set;}
        /// <summary>
        /// 灾情种类编码
        /// </summary>
        public virtual String KindCode { get; set; }
        /// <summary>
        /// 灾情图标
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 灾情种类父级Id
        /// </summary>
        public virtual Guid? Pid { get; set; }
    }
}
