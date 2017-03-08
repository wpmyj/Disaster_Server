using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.CityService.Dto
{
    [AutoMap(typeof(CommunityCodeTb))]
    public class CommunityOutput : EntityDto<Int64>
    {
        /// <summary>
        /// 社区名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 父节点Id
        /// </summary>
        public virtual List<CityOutput> Parent { get; set; }
    }
}
