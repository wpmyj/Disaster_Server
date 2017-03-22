using Abp.Application.Services;
using DisasterReport.DtoTemplate;
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
        /// <summary>
        /// 删除团队成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        MessageGroupOutput DeleteGroupMember(GroupMemeberDeleteInput input);
        /// <summary>
        /// 分页得到团队
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<MessageGroupOutput> GetPageMessageGroup(int pageIndex = 1, int pageSize = 9999);
        /// <summary>
        /// 通过团队id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        MessageGroupOutput GetMessageGroupById(Guid id, int pageIndex = 1, int pageSize = 9999);
        /// <summary>
        /// 获取上报人所属团队信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        MessageGroupOutput GetMessageGroupByReporterId(Guid id);
        /// <summary>
        /// 分页获取消息组成员
        /// </summary>
        /// <param name="messageGroupId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">默认9999</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<ReporterMemberOutput> GetPageGroupMember(Guid messageGroupId, int pageIndex = 1, int pageSize = 9999);
        /// <summary>
        /// 分页获取不在此消息组里的其他上报人员
        /// </summary>
        /// <param name="messageGroupId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">默认9999</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<ReporterGroupOutput> GetOtherNoGroupMember(Guid messageGroupId, int pageIndex = 1, int pageSize = 9999);
        /// <summary>
        /// 得到消息组的灾情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        List<MessageDisasterOutput> GetMessageGroupDisaster(Guid id);
        /// <summary>
        /// 修改队伍图标
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        void UpdateMessageGroupIcon(UpdateMessageGroupIconInput input);
    }
}
