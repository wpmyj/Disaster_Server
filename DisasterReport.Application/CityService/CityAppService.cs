using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisasterReport.CityService.Dto;
using DisasterReport.DomainEntities;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;

namespace DisasterReport.CityService
{
    public class CityAppService : DisasterReportAppServiceBase, ICityAppService
    {
        private readonly IRepository<CommunityCodeTb, Int64> _communityCodeRepo;
        private readonly IRepository<CityCodeTb, Int64> _cityCodeTbRepo;

        public CityAppService(
                IRepository<CommunityCodeTb, Int64> communityCodeRepo,
                IRepository<CityCodeTb, Int64> cityCodeTbRepo
            )
        {
            _communityCodeRepo = communityCodeRepo;
            _cityCodeTbRepo = cityCodeTbRepo;
        }

        public CommunityOutput GetCommunityInfoByName(string name)
        {
            var queryName = name.Replace("社区", "");
            var communityResult = _communityCodeRepo.FirstOrDefault(c => c.Name.Contains(queryName));

            if(communityResult != null)
            {
                CommunityOutput outResult = new CommunityOutput()
                {
                    Id = communityResult.Id,
                    Name = communityResult.Name,
                    Parent = new List<CityOutput>()
                };
                // 区/街道
                var parentCode = _cityCodeTbRepo.FirstOrDefault(c => c.Id == communityResult.Pid);

                outResult.Parent.Add(new CityOutput()
                {
                    Id = parentCode.Id,
                    Name = parentCode.Name,
                    Type = parentCode.Type
                });

                while (parentCode.Pid != 0)
                {
                    parentCode = _cityCodeTbRepo.FirstOrDefault(c => c.Id == parentCode.Pid);
                    outResult.Parent.Add(new CityOutput()
                    {
                        Id = parentCode.Id,
                        Name = parentCode.Name,
                        Type = parentCode.Type
                    });
                }

                return outResult;
            }

            throw new UserFriendlyException("没有对应的社区");
        }
    }
}
