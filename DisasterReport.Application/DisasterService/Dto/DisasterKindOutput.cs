using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;

namespace DisasterReport.DisasterService.Dto
{
    [AutoMap(typeof(DisasterKindTb))]
    public class DisasterKindOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 灾情种类名称
        /// </summary>
        public virtual String Name { get; set; }
        /// <summary>
        /// 灾情种类编码
        /// </summary>
        public virtual String KindCode { get; set; }
        /// <summary>
        /// 灾情种类父级Id
        /// </summary>
        public virtual String Pid { get; set; }
    }
}
