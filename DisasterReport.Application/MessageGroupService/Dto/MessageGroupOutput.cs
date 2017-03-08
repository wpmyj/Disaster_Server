using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using DisasterReport.ReporterService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.MessageGroupService.Dto
{
    [AutoMap(typeof(MessageGroupTb))]
    public class MessageGroupOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 消息组名称
        /// </summary>
        public virtual string GroupName { get; set; }
        /// <summary>
        /// 消息组介绍
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 消息组的头像
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 消息组总人数
        /// </summary>
        public virtual int GroupTotalNum { get; set; }
        /// <summary>
        /// 1）救援队 2）个人团队  两类节点
        /// </summary>
        public virtual int type { get; set; }
        /// <summary>
        /// 关联的成员
        /// </summary>
        public virtual ICollection<ReporterMemberOutput> Member { get; set; }
    }
}
