using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisasterReport.DeviceService.Dto;
using Abp.Domain.Repositories;
using DisasterReport.DomainEntities;
using Abp.AutoMapper;
using Abp.UI;
using DisasterReport.ReporterService.Dto;
using DisasterReport.DtoTemplate;

namespace DisasterReport.DeviceService
{
    public class DeviceAppService : DisasterReportAppServiceBase, IDeviceAppService
    {
        private readonly IRepository<DeviceInfoTb, Guid> _deviceInfoTbRepo;
        private readonly IRepository<ReporterInfoTb, Guid> _reporterInfoTbRepo;

        public DeviceAppService(
                IRepository<DeviceInfoTb, Guid> deviceInfoTbRepo,
                IRepository<ReporterInfoTb, Guid> reporterInfoTbRepo
            )
        {
            _deviceInfoTbRepo = deviceInfoTbRepo;
            _reporterInfoTbRepo = reporterInfoTbRepo;
        }

        public async Task<DeviceOutput> AddDevice(DeviceAddInput input)
        {
            var existDevice = _deviceInfoTbRepo.FirstOrDefault(d => d.DeviceCode == input.DeviceCode);
            if(existDevice != null)
            {
                throw new UserFriendlyException("已存在此编号的终端设备");
            }

            var existReporter = _reporterInfoTbRepo.FirstOrDefault(r => r.Id == input.ReporterId);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有此上报人员");
            }

            var addDevice = new DeviceInfoTb()
            {
                AreaAddress = input.AreaAddress,
                AreaCode = input.AreaCode,
                DeviceCode = input.DeviceCode,
                ProduceAddress = input.ProduceAddress,
                ProduceDate = input.ProduceDate,
                Reporter = existReporter,
                Type = input.Type
            };

            var id = await _deviceInfoTbRepo.InsertAndGetIdAsync(addDevice);
            addDevice.Id = id;

            return addDevice.MapTo<DeviceOutput>();
        }

        public void BindReporter(DeviceBindInput input)
        {
            var existReporter = _reporterInfoTbRepo.FirstOrDefault(r => r.Id == input.ReporterId);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有找到对应的上报人员");
            }

            var existDevice = _deviceInfoTbRepo.FirstOrDefault(d => d.DeviceCode == input.DeviceCode);
            if(existDevice == null)
            {
                throw new UserFriendlyException("没有对应的设备");
            }

            if(existDevice.Reporter != null)
            {
                throw new UserFriendlyException("此设备已经绑定其他上报人员");
            }

            existDevice.Reporter = existReporter;

            _deviceInfoTbRepo.Update(existDevice);
        }

        public async Task<DeviceOutput> GetDeviceByReporterId(Guid id)
        {
            // 判断所传ID是否是上报者的id
            var existReporter = _reporterInfoTbRepo.FirstOrDefault(r => r.Id == id);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有此用户");
            }
            var result = await _deviceInfoTbRepo.FirstOrDefaultAsync(d => d.Reporter.Id == id);
            if(result != null)
            {
                return new DeviceOutput()
                {
                    AreaAddress = result.AreaAddress,
                    AreaCode = result.AreaCode,
                    DeviceCode = result.DeviceCode,
                    Id = result.Id,
                    Reporter = existReporter.MapTo<ReporterOutput>(),
                    ProduceAddress = result.ProduceAddress,
                    ProduceDate = result.ProduceDate,
                    Type = result.Type
                };
            }
            else
            {
                throw new UserFriendlyException("此用户没有绑定设备");
            }
        }

        public RuimapPageResultDto<DeviceOutput> GetPageDevice(int pageIndex = 1, int pageSize = 10)
        {
            var count = _deviceInfoTbRepo.Count();

            var result = _deviceInfoTbRepo.GetAll().OrderBy(d=>d.DeviceCode).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<DeviceOutput>(count, currPage, totalPage, result.MapTo<List<DeviceOutput>>());
        }
    }
}
