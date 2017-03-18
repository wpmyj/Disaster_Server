using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;

namespace DisasterReport.DisasterService.Dto
{
    [AutoMap(typeof(DisasterInfoTb))]
    public class ReportDisasterInput
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual string DeviceCode { get; set; }
        /// <summary>
        /// 上报人员Id
        /// </summary>
        public virtual Guid ReporterId { get; set; }
        /// <summary>
        /// 上报灾情类型编码
        /// </summary>
        public virtual String DisasterKindCode { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public virtual string AreaCode { get; set; }
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
        /// 上传灾情附件-图片
        /// </summary>
        public virtual List<FileUploadResult> UploadFiles { get; set; }
    }
}
