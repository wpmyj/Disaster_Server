using System.Threading.Tasks;
using Abp.Application.Services;
using DisasterReport.Roles.Dto;

namespace DisasterReport.Roles
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
