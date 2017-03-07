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
            var addReoirterObj = new ReporterInfoTb()
            {
                Address = input.Address,
                AreaCode = input.AreaCode,
                Name = input.Name,
                Phone = input.Phone,
                UserId = input.UserId
            };

            var id = await _reporterInfoTbRepo.InsertAndGetIdAsync(addReoirterObj);
            addReoirterObj.Id = id;

            return addReoirterObj.MapTo<ReporterOutput>();
        }

        public void BindUserAccount(ReporterBindInput input)
        {
            // 优先考虑UserId
            var existUser = _userTbRepo.FirstOrDefault(u => u.Id == input.UserId || u.UserCode == input.UserCode);
            if(existUser == null)
            {
                throw new UserFriendlyException(string.Format("没有存在相应的账号"));
            }

            var existReporter = _reporterInfoTbRepo.FirstOrDefault(r => r.Id == input.ReporterId);
            if(existReporter == null)
            {
                throw new UserFriendlyException(string.Format("没有存在相应的上报人员"));
            }

            if(existReporter.UserId.ToString() == "")
            {
                throw new UserFriendlyException(string.Format("此上报人员已绑定账号"));
            }

            // 更新绑定账号
            existReporter.UserId = existUser.Id;
            _reporterInfoTbRepo.Update(existReporter);
        }

        public RuimapPageResultDto<ReporterOutput> GetPageReporter(int pageIndex = 1, int pageSize = 9999)
        {
            var count = _reporterInfoTbRepo.Count();

            var result = _reporterInfoTbRepo.GetAll().OrderBy(e => e.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<ReporterOutput>(count, currPage, totalPage, result.MapTo<List<ReporterOutput>>());
        }

        public async Task<ReporterOutput> GetReporterById(ReporterUnBindInput input)
        {
            var existReporter = await _reporterInfoTbRepo.FirstOrDefaultAsync(r => r.Id == input.ReporterId);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有此上报人员");
            }
            return existReporter.MapTo<ReporterOutput>();
        }

        public void UnBindUserAccoutn(ReporterUnBindInput input)
        {
            throw new NotImplementedException();
        }
    }
}
