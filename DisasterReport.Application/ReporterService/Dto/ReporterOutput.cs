﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.ReporterService.Dto
{
    [AutoMap(typeof(ReporterInfoTb))]
    public class ReporterOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual String Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual String Phone { get; set; }
        /// <summary>
        /// 所属区域编码
        /// </summary>
        public virtual String AreaCode { get; set; }
        /// <summary>
        /// 所在地址
        /// </summary>
        public virtual String Address { get; set; }
        /// <summary>
        /// 头像图片
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 最后一次上报的位置
        /// </summary>
        public virtual string LastAddress { get; set; }
        /// <summary>
        /// 最后一次上报的经度
        /// </summary>
        public virtual double LastLng { get; set; }
        /// <summary>
        /// 最后一次上报的维度
        /// </summary>
        public virtual double LastLat { get; set; }
        /// <summary>
        /// 上报人员所关联的用户Id
        /// </summary>
        public virtual Guid UserId { get; set; }
    }
}
