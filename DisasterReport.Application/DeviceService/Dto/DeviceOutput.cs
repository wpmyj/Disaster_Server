using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using DisasterReport.ReporterService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DeviceService.Dto
{
    [AutoMap(typeof(DeviceInfoTb))]
    public class DeviceOutput : EntityDto<Guid>
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
        /// 上报人员Id
        /// </summary>
        public virtual ReporterOutput Reporter { get; set; }
    }
}
