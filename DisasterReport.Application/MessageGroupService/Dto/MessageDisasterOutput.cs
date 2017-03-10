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
    [AutoMap(typeof(DisasterInfoTb))]
    public class MessageDisasterOutput:EntityDto<Guid>
    {
        /// <summary>
        /// 灾情代码
        /// </summary>
        public virtual string DisasterCode { get; set; }
        /// <summary>
        /// 上报时间
        /// </summary>
        public virtual DateTime ReportDate { get; set; }
        /// <summary>
        /// 上报灾情类型
        /// </summary>
        public virtual DisasterKindTb DisasterKind { get; set; }
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
        /// 灾情是否已处理 0-没有处理 1-正在处理 2-已处理
        /// </summary>
        public virtual int Status { get; set; }
    }
}
