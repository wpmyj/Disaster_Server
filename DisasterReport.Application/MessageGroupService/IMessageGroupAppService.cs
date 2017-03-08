using Abp.Application.Services;
using DisasterReport.MessageGroupService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DisasterReport.MessageGroupService
{
    /// <summary>
    /// 团队接口
    /// </summary>
    public interface IMessageGroupAppService : IApplicationService
    {
        /// <summary>
        /// 增加团队
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        MessageGroupOutput AddMessageGroup(MessageGroupAddInput input);
        /// <summary>
        /// 给团队添加成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MessageGroupOutput AddGroupMember(GroupMemberAddInput input);
    }
}
