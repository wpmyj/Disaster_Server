using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    /// <summary>
    /// 灾情与人员抢救关系表 用于表示哪些人员参与了哪些灾情
    /// </summary>
    [Table("DR_DisasterRescueTb")]
    public class DisasterRescueTb: Entity<Guid>
    {
        /// <summary>
        /// 参与的灾情
        /// </summary>
        public virtual DisasterInfoTb Disaster { get; set; }
        /// <summary>
        /// 参与人员
        /// </summary>
        public virtual ReporterInfoTb Reporter { get; set; }
        /// <summary>
        /// 开始参与时间
        /// </summary>
        public virtual DateTime StartTime { get; set; }
    }
}
