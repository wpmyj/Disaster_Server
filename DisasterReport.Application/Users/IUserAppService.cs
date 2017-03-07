using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DisasterReport.Users.Dto;

namespace DisasterReport.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task<PagedResultDto<UserListDto>> GetPageUsers(int pageIndex=1,int pageSize=10);

        Task CreateUser(CreateUserInput input);
    }
}