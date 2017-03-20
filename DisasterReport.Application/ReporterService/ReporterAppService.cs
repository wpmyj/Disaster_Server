using Abp.Domain.Repositories;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisasterReport.ReporterService.Dto;
using Abp.AutoMapper;
using Abp.UI;
using DisasterReport.DtoTemplate;

namespace DisasterReport.ReporterService
{
    public class ReporterAppService : DisasterReportAppServiceBase, IReporterAppService
    {
        private readonly IRepository<ReporterInfoTb, Guid> _reporterInfoTbRepo;
        private readonly IRepository<UserTb, Guid> _userTbRepo;

        public ReporterAppService(
                IRepository<ReporterInfoTb, Guid> reporterInfoTbRepo,
                IRepository<UserTb, Guid> userTbRepo
            )
        {
            _reporterInfoTbRepo = reporterInfoTbRepo;
            _userTbRepo = userTbRepo;
        }

        public async Task<ReporterOutput> AddReporter(ReporterAddInput input)
        {
            // 判断用户是否被占用
            var existUser = _userTbRepo.FirstOrDefault(u => u.Id == input.UserId);
            if(existUser == null || existUser.Enable == true)
            {
                throw new UserFriendlyException("用户不存在、或被占用");
            }

            var addReoirterObj = new ReporterInfoTb()
            {
                Address = input.Address,
                AreaCode = input.AreaCode,
                Name = input.Name,
                Phone = input.Phone,
                User = existUser,
                Age = input.Age,
                Remark = input.Remark,
                Type = input.Type
            };

            var id = await _reporterInfoTbRepo.InsertAndGetIdAsync(addReoirterObj);
            addReoirterObj.Id = id;

            existUser.Enable = true;

            _userTbRepo.Update(existUser);

            return addReoirterObj.MapTo<ReporterOutput>();
        }
        
        public RuimapPageResultDto<ReporterOutput> GetPageReporter(int type = 9, int pageIndex = 1, int pageSize = 9999, int hasDevice = 9)
        {
            if(type == 9)
            {
                if(hasDevice == 9)
                {
                    var count = _reporterInfoTbRepo.Count();

                    var result = _reporterInfoTbRepo.GetAll().OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int currPage = pageIndex;
                    int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

                    return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
                }
                else
                {
                    if(hasDevice == 1)
                    {
                        var count = _reporterInfoTbRepo.Count(r => r.Device != null);

                        var result = _reporterInfoTbRepo.GetAll().Where(r => r.Device != null).OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                        int currPage = pageIndex;
                        int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));
                        return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
                    }
                    else if (hasDevice == 2)
                    {
                        var count = _reporterInfoTbRepo.Count(r => r.Device == null);

                        var result = _reporterInfoTbRepo.GetAll().Where(r => r.Device == null).OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                        int currPage = pageIndex;
                        int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));
                        return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
                    }
                    throw new UserFriendlyException("出错了");
                }
            }
            else
            {
                if (hasDevice == 9)
                {
                    var count = _reporterInfoTbRepo.Count(r => r.Type == type);

                    var result = _reporterInfoTbRepo.GetAll().Where(r => r.Type == type).OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int currPage = pageIndex;
                    int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

                    return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
                }
                else
                {
                    if (hasDevice == 1)
                    {
                        var count = _reporterInfoTbRepo.Count(r => r.Device != null && r.Type == type);

                        var result = _reporterInfoTbRepo.GetAll().Where(r => r.Device != null && r.Type == type).OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                        int currPage = pageIndex;
                        int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));
                        return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
                    }
                    else if (hasDevice == 2)
                    {
                        var count = _reporterInfoTbRepo.Count(r => r.Device == null && r.Type == type);

                        var result = _reporterInfoTbRepo.GetAll().Where(r => r.Device == null && r.Type == type).OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                        int currPage = pageIndex;
                        int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));
                        return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
                    }
                    throw new UserFriendlyException("出错了");
                }
            }
        }

        public async Task<ReporterOutput> GetReporterById(Guid id)
        {
            var existReporter = await _reporterInfoTbRepo.FirstOrDefaultAsync(r => r.Id == id);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有此上报人员");
            }
            return existReporter.MapTo<ReporterOutput>();
        }

        public RuimapPageResultDto<ReporterOutput> GetReporterByNameOrPhone(string nameOrPhone, int pageIndex = 1, int pageSize = 9999)
        {
            var count = _reporterInfoTbRepo.Count(r => r.Name.Contains(nameOrPhone) || r.Phone.Contains(nameOrPhone));

            var result = _reporterInfoTbRepo.GetAll().Where(r => r.Name.Contains(nameOrPhone) || r.Phone.Contains(nameOrPhone)).OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
        }
        
        public async Task<ReporterOutput> UpdateReporter(ReporterUpdateInput input)
        {
            // 先找到对应的用户记录Row
            var existReporter = await _reporterInfoTbRepo.FirstOrDefaultAsync(r => r.Id == input.ReporterId);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有相应的人员");
            }
            existReporter.Name = input.Name;
            existReporter.Phone = input.Phone;
            existReporter.Photo = input.Photo;
            existReporter.Type = input.Type;
            existReporter.Age = input.Age;
            existReporter.Address = input.Address;

            // 找到对应的用户账号
            var existUser = await _userTbRepo.FirstOrDefaultAsync(u => u.Id == existReporter.User.Id);
            if(existUser == null)
            {
                throw new UserFriendlyException("没有相应的账号");
            }

            existUser.Password = input.Password;

            _reporterInfoTbRepo.Update(existReporter);
            _userTbRepo.Update(existUser);

            var newReporter = _reporterInfoTbRepo.FirstOrDefault(r => r.Id == input.ReporterId);

            return new ReporterOutput()
            {
                Id = existReporter.Id,
                Address = existReporter.Address,
                Age = existReporter.Age,
                AreaCode = existReporter.AreaCode,
                LastAddress = existReporter.LastAddress,
                LastLat = existReporter.LastLat,
                LastLng = existReporter.LastLng,
                Name = existReporter.Name,
                Phone = existReporter.Phone,
                Photo = existReporter.Photo,
                Remark = existReporter.Remark
            };
        }

        public void UpdateReporterLastPos(UpdateReporterLastPosInput input)
        {
            var existReporter = _reporterInfoTbRepo.FirstOrDefault(r => r.Id == input.Id);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有此上报人员");
            }
            existReporter.LastLng = input.Lng;
            existReporter.LastLat = input.Lat;
            existReporter.LastAddress = input.Address;
            _reporterInfoTbRepo.Update(existReporter);
        }
    }
}
