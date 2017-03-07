using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_DevBindReporterTb")] // 设备与人员绑定表
    public class DevBindReporterTb : Entity<Guid>
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual int DeviceCode { get; set; }
        /// <summary>
        /// 上报人员Id
        /// </summary>
        public virtual Guid ReporterId { get; set; }
    }
}
