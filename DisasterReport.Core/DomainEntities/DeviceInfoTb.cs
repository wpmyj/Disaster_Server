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
        /// 设备所在地址
        /// </summary>
        public virtual string AreaAddress { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual int DeviceCode { get; set; }
        /// <summary>
        /// 上报人员Id
        /// </summary>
        public virtual Guid ReporterId { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 设备生产日期
        /// </summary>
        public virtual DateTime ProduceDate { get; set; }
        /// <summary>
        /// 设备生产地址
        /// </summary>
        public virtual string ProduceAddress { get; set; }
    }
}
