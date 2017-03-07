using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisasterReport.DisasterService.Dto;
using Abp.Domain.Repositories;
using DisasterReport.DomainEntities;
using Abp.AutoMapper;
using DisasterReport;
using DisasterReport.DisasterService;
using DisasterReport.DtoTemplate;
using DisasterReport.DisasterService.Event;
using Abp.Events.Bus;
using Abp.UI;

namespace DisasterAppService.DisasterService
{
    public class DisasterAppService : DisasterReportAppServiceBase, IDisasterAppService
    {
        private readonly IRepository<DisasterInfoTb, Guid> _disasterInfoTbRepository;
        private readonly IRepository<UploadsFileTb, Guid> _uploadsFileTbRespository;

        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DisasterAppService(
                IRepository<DisasterInfoTb, Guid> disasterInfoTbRepository,
                IRepository<UploadsFileTb, Guid> uploadsFileTbRespository
            )
        {
            EventBus = NullEventBus.Instance;
            _disasterInfoTbRepository = disasterInfoTbRepository;
            _uploadsFileTbRespository = uploadsFileTbRespository;
        }

        /// <summary>
        /// 分页获取灾情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type">上报类型 1-移动网络 2-北斗短报文 9-全部</param>
        /// <param name="status">灾情是否已处理 0-没有处理 1-正在处理 2-已处理 9-全部</param>
        /// <returns></returns>
        public RuimapPageResultDto<ReportDisasterOutput> GetPageDisaster(int pageIndex = 1, int pageSize = 9999, int type = 9, int status = 9)
        {
            var count = 0;
            List<DisasterInfoTb> result;
            if (type == 9 && status == 9)
            {
                count = _disasterInfoTbRepository.Count();
                result = _disasterInfoTbRepository.GetAll().OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            } else  if(type == 9 && status != 9)
            {
                count = _disasterInfoTbRepository.Count(d => d.Status == status);
                result = _disasterInfoTbRepository.GetAll().Where(d => d.Status == status).OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            } else if(type != 9 && status == 9)
            {
                count = _disasterInfoTbRepository.Count(d => d.Type == type);
                result = _disasterInfoTbRepository.GetAll().Where(d => d.Type == type).OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            } else
            {
                count = _disasterInfoTbRepository.Count(d => d.Type == type && d.Status == status);
                result = _disasterInfoTbRepository.GetAll().Where(d => d.Type == type && d.Status == status).OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<ReportDisasterOutput>(count, currPage, totalPage, result.MapTo<List<ReportDisasterOutput>>());
        }

        /// <summary>
        /// 手持终端上报灾情
        /// </summary>
        /// <param name="input"></param>
        public void ReportDisaster(ReportDisasterInput input)
        {
            var uploads = input.UploadFiles;
            var disaster = input.MapTo<DisasterInfoTb>();
            disaster.Status = 0;    // 没有处理
            disaster.ReportDate = DateTime.Now;

            var id = _disasterInfoTbRepository.InsertAndGetId(disaster);

            for(var i = 0; i < uploads.Count(); i++)
            {
                var fileInsertInfo = new UploadsFileTb
                {
                    Id = Guid.NewGuid(),
                    DisasterInfoId = id,
                    FileName = uploads[i].FileName,
                    Path = uploads[i].FilePath
                };
                _uploadsFileTbRespository.Insert(fileInsertInfo);
            }

            // 推送新增灾情Id给web管理终端
            EventBus.Trigger(new DisasterReportEventData() { DisasterInfoId = id });
        }

        public async Task<ReportDisasterOutput> GetDisasterById(Guid id)
        {
            var result = await _disasterInfoTbRepository.FirstOrDefaultAsync(d => d.Id == id);
            if(result == null)
            {
                throw new UserFriendlyException("查询不到相应的灾情");
            }
            return result.MapTo<ReportDisasterOutput>();
        }

        public List<UploadsFileTbOutput> GetDisasterFilePicById(Guid id)
        {
            List<UploadsFileTb> result;

            result = _uploadsFileTbRespository.GetAllList(u=>u.DisasterInfoId == id);

            return result.MapTo<List<UploadsFileTbOutput>>();
        }
    }
}
