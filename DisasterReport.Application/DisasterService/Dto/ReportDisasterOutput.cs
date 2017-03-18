using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using DisasterReport.ReporterService.Dto;
using System;

namespace DisasterReport.DisasterService.Dto
{
    [AutoMap(typeof(DisasterInfoTb))]
    public class ReportDisasterOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 灾情编号-后台生成
        /// </summary>
        public virtual string DisasterCode { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual string DeviceCode { get; set; }
        /// <summary>
        /// 上报时间
        /// </summary>
        public virtual DateTime ReportDate { get; set; }
        /// <summary>
        /// 上报人员
        /// </summary>
        public virtual ReporterOutput Reporter { get; set; }
        /// <summary>
        /// 上报灾情类型编码
        /// </summary>
        public virtual DisasterKindOutput DisasterKind { get; set; }
        /// <summary>
        /// 灾情位置
        /// </summary>
        public virtual String DisasterAddress { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public virtual Double Lng { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public virtual Double Lat { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual String Remark { get; set; }
        /// <summary>
        /// 上报类型 1-移动网络 2-北斗短报文
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 灾情是否已处理 0-没有处理 1-正在处理 2-已处理 9-全部
        /// </summary>
        public virtual int Status { get; set; }
    }
}
