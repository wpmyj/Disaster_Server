using Abp.Application.Services;
using DisasterReport.ReporterService.Dto;
using DisasterReport.UserAccount.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DisasterReport.UserAccount
{
    /// <summary>
    /// 账户接口
    /// </summary>
    public interface IUserAccountAppService : IApplicationService
    {
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        Task<UserAccountOutput> Login(UserAccountLoginInput input);
        /// <summary>
        /// 根据用户id返回用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        Task<UserAccountOutput> GetUserAccountById(Guid id);
        /// <summary>
        /// 添加用户账号密码
        /// </summary>
        /// <param name="input">Role： 0-普通用户，1-管理员用户</param>
        /// <returns></returns>
        [HttpPost]
        Task<UserTbOutputDto> AddUserAccount(UserAccountAddUserInput input);
    }
}
