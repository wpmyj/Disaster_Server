using Abp.Domain.Repositories;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisasterReport.UserAccount.Dto;
using Abp.UI;
using Abp.AutoMapper;

namespace DisasterReport.UserAccount
{
    public class UserAccountAppService: DisasterReportAppServiceBase, IUserAccountAppService
    {
        private readonly IRepository<ReporterInfoTb, Guid> _reporterInfoRepo;
        private readonly IRepository<UserTb, Guid> _userRepo;

        public UserAccountAppService(
                IRepository<ReporterInfoTb, Guid> reporterInfoRepo,
                IRepository<UserTb, Guid> userRepo
            )
        {
            _reporterInfoRepo = reporterInfoRepo;
            _userRepo = userRepo;
        }

        public async Task<UserTbOutputDto> AddUserAccount(UserAccountAddUserInput input)
        {
            var existUser = _userRepo.FirstOrDefault(u => u.UserCode == input.UserCode);
            if(existUser != null)
            {
                throw new UserFriendlyException(string.Format("{0}此账户名已被占用", input.UserCode));
            }

            var addUser = new UserTb()
            {
                Enable = false,
                Password = input.Password,
                UserCode = input.UserCode
            };

            var id = await _userRepo.InsertAndGetIdAsync(addUser);
            addUser.Id = id;

            return addUser.MapTo<UserTbOutputDto>();
        }

        public async Task<UserAccountOutput> AdminLogin(UserAccountLoginInput input)
        {
            var existUser = await _userRepo.FirstOrDefaultAsync(u => u.UserCode == input.UserCode);

            if (existUser == null)
            {
                throw new UserFriendlyException("验证失败，没有此用户");
            }

            if (existUser.Password != input.Password)
            {
                throw new UserFriendlyException("验证失败，密码错误");
            }

            if (existUser.Enable != true)
            {
                throw new UserFriendlyException("错误，此用户正被禁用中");
            }

            // 验证账号密码通过
            // 获取此用户的上报人员信息
            var existReporter = await _reporterInfoRepo.FirstOrDefaultAsync(r => r.User.Id == existUser.Id);
            if (existReporter == null)
            {
                throw new UserFriendlyException("错误，此用户没有绑定上报用员，请联系管理员");
            }

            // 判断是否是上报人员类型
            // 如果不是上报人员类型
            if (existReporter.Type != 2)
            {
                throw new UserFriendlyException("错误，此用户不是后台用户");
            }

            var outResult = new UserAccountOutput
            {
                Id = existReporter.Id,
                Name = existReporter.Name,
                Phone = existReporter.Phone,
                Address = existReporter.Address,
                AreaCode = existReporter.AreaCode,
                Photo = existReporter.Photo,
                Age = existReporter.Age,
                Remark = existReporter.Remark,
                Type = existReporter.Type,
                User = existUser.MapTo<UserTbOutputDto>()
            };
            return outResult;
        }

        public async Task<UserAccountOutput> GetUserAccountById(Guid id)
        {
            var existUser = _userRepo.FirstOrDefault(u => u.Id == id);
            ReporterInfoTb existReporter = null;
            if (existUser == null)
            {
                existReporter = _reporterInfoRepo.FirstOrDefault(r => r.Id == id);
                if(existReporter == null)
                {
                    throw new UserFriendlyException("没有此用户账号信息");
                }
            }
            if(existReporter == null)
            {
                var bindReporter = await _reporterInfoRepo.FirstOrDefaultAsync(r => r.User.Id == existUser.Id);
                if(bindReporter == null)
                {
                    return new UserAccountOutput()
                    {
                        User = existUser.MapTo<UserTbOutputDto>()
                    };
                }
                var result = new UserAccountOutput()
                {
                    Address = bindReporter.Address,
                    AreaCode = bindReporter.AreaCode,
                    Id = bindReporter.Id,
                    Name = bindReporter.Name,
                    Phone = bindReporter.Phone,
                    Photo = bindReporter.Photo,
                    Age = bindReporter.Age,
                    Remark = bindReporter.Remark,
                    Type = bindReporter.Type,
                    User = existUser.MapTo<UserTbOutputDto>()
                };

                return result;
            }
            else
            {
                var result = new UserAccountOutput()
                {
                    Address = existReporter.Address,
                    AreaCode = existReporter.AreaCode,
                    Id = existReporter.Id,
                    Name = existReporter.Name,
                    Phone = existReporter.Phone,
                    Photo = existReporter.Photo,
                    Age = existReporter.Age,
                    Remark = existReporter.Remark,
                    Type = existReporter.Type,
                    User = existReporter.User.MapTo<UserTbOutputDto>()
                };
                return result;
            }
        }

        public async Task<UserAccountOutput> Login(UserAccountLoginInput input)
        {
            var existUser = await _userRepo.FirstOrDefaultAsync(u => u.UserCode == input.UserCode);

            if(existUser == null)
            {
                throw new UserFriendlyException("验证失败，没有此用户");
            }

            if(existUser.Password != input.Password)
            {
                throw new UserFriendlyException("验证失败，密码错误");
            }

            if(existUser.Enable != true)
            {
                throw new UserFriendlyException("错误，此用户正被禁用中");
            }
            
            // 验证账号密码通过
            // 获取此用户的上报人员信息
            var existReporter = await _reporterInfoRepo.FirstOrDefaultAsync(r => r.User.Id == existUser.Id);
            if(existReporter == null)
            {
                throw new UserFriendlyException("错误，此用户没有绑定上报用员，请联系管理员");
            }

            // 判断是否是上报人员类型
            // 如果不是上报人员类型
            if(existReporter.Type != 1)
            {
                throw new UserFriendlyException("错误，此用户不是上报人员");
            }

            var outResult = new UserAccountOutput
            {
                Id = existReporter.Id,
                Name = existReporter.Name,
                Phone = existReporter.Phone,
                Address = existReporter.Address,
                AreaCode = existReporter.AreaCode,
                Photo = existReporter.Photo,
                Age = existReporter.Age,
                Remark = existReporter.Remark,
                Type = existReporter.Type,
                User = existUser.MapTo<UserTbOutputDto>()
            };
            return outResult;
        }
    }
}
