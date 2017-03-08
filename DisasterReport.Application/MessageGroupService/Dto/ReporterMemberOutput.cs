using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageGroupService.Dto
{

    [AutoMap(typeof(GroupMemberTb))]
    public class ReporterMemberOutput
    {
        /// <summary>
        /// 成员类型
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 上报人员
        /// </summary>
        public virtual ReporterGroupOutput Reporter { get; set; }
    }
}
