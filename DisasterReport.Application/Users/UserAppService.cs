using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DisasterReport.Authorization;
using DisasterReport.Users.Dto;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace DisasterReport.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : DisasterReportAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await UserManager.CreateAsync(user));
        }

        Task<ListResultDto<UserListDto>> IUserAppService.GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<UserListDto>> GetPageUsers(int pageIndex = 1, int pageSize = 10)
        {
            

            var count = await _userRepository.CountAsync();
            var users = _userRepository.GetAll().OrderBy(u => u.Surname).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var pageResult = new PagedResultDto<UserListDto>(count, users.MapTo<List<UserListDto>>());
            //{
            //    TotalCount = count,
            //    Items = users.MapTo<List<UserListDto>>()
            //};


            return pageResult;
        }
    }
}