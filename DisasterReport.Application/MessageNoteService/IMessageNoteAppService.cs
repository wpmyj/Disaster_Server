using Abp.Application.Services;
using DisasterReport.DtoTemplate;
using DisasterReport.MessageNoteService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DisasterReport.MessageNoteService
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public interface IMessageNoteAppService: IApplicationService
    {
        /// <summary>
        /// 新增推送消息
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        MessageNoteOutput PostMessage(AddMessageInput input);
        /// <summary>
        /// 分页得到消息列表
        /// </summary>
        /// <param name="title">消息title模糊搜索使用</param>
        /// <param name="flag">默认 9（全部）1 普通 2重要 3紧急</param>
        /// <param name="reporterId">默认 全部</param>
        /// <param name="pageIndex">默认 1</param>
        /// <param name="pageSize">默认 9999</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<MessageNoteOutput> GetPageMessageNote(string title = "", int flag = 9, string reporterId = "", int pageIndex = 1, int pageSize = 9999);
        /// <summary>
        /// 通过Id获取当前的消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        MessageNoteOutput GetMessageNoteById(Guid id);
        /// <summary>
        /// 删除上报人员所接收的消息推送
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        void DeleteReporterMessageNote(DeleteReporterMessageNoteInput input);
    }
}
