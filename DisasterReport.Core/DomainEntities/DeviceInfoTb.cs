using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_DeviceInfoTb")] //设备信息表
    public class DeviceInfoTb: Entity<Guid>
    {
        /// <summary>
        /// 所属区域编号
        /// </summary>
        public virtual string AreaCode { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual int DeviceCode { get; set; }
        /// <summary>
        /// 上报人员
        /// </summary>
        public virtual ReporterInfoTb Reporter { get; set; }
    }
}
