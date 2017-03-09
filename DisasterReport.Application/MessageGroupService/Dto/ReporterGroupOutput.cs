using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageGroupService.Dto
{
    [AutoMap(typeof(ReporterInfoTb))]
    public class ReporterGroupOutput: EntityDto<Guid>
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
        /// 年龄
        /// </summary>
        public virtual int Age { get; set; }
        /// <summary>
        /// 所在地址
        /// </summary>
        public virtual String Address { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 人员备注
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
    }
}
