using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepository<ReporterInfoTb, Guid> _reporterInfoRespository;
        private readonly IRepository<DisasterKindTb, Guid> _disasterKindTbRespository;
        private readonly IRepository<MessageGroupTb, Guid> _messageGroupRespository;

        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DisasterAppService(
                IRepository<DisasterInfoTb, Guid> disasterInfoTbRepository,
                IRepository<UploadsFileTb, Guid> uploadsFileTbRespository,
                IRepository<ReporterInfoTb, Guid> reporterInfoRespository,
                IRepository<DisasterKindTb, Guid> disasterKindTbRespository,
                IRepository<MessageGroupTb, Guid> messageGroupRespository
            )
        {
            EventBus = NullEventBus.Instance;
            _disasterInfoTbRepository = disasterInfoTbRepository;
            _uploadsFileTbRespository = uploadsFileTbRespository;
            _reporterInfoRespository = reporterInfoRespository;
            _disasterKindTbRespository = disasterKindTbRespository;
            _messageGroupRespository = messageGroupRespository;
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
            disaster.DisasterCode = "[DC]-" + Guid.NewGuid();

            var existReporter = _reporterInfoRespository.FirstOrDefault(r => r.Id == input.ReporterId);
            if(existReporter == null)
            {
                throw new UserFriendlyException("没有对应的上报人员");
            }

            disaster.Reporter = existReporter;

            var existDisasterKind = _disasterKindTbRespository.FirstOrDefault(d => d.KindCode == input.DisasterKindCode);
            if(existDisasterKind == null)
            {
                throw new UserFriendlyException("没有对应的灾情类型");
            }

            disaster.DisasterKind = existDisasterKind;

            var id = _disasterInfoTbRepository.InsertAndGetId(disaster);

            for(var i = 0; i < uploads.Count(); i++)
            {
                var fileInsertInfo = new UploadsFileTb
                {
                    OtherRowId = id,
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

            result = _uploadsFileTbRespository.GetAllList(u=>u.OtherRowId == id);

            return result.MapTo<List<UploadsFileTbOutput>>();
        }

        public RuimapPageResultDto<ReportDisasterOutput> GetPageDisasterByReporterId(Guid id, int pageIndex = 1, int pageSize = 9999, int type = 9, int status = 9)
        {
            var count = 0;
            List<DisasterInfoTb> result;
            if (type == 9 && status == 9)
            {
                count = _disasterInfoTbRepository.Count(d => d.Reporter.Id == id);
                result = _disasterInfoTbRepository.GetAll().Where(d => d.Reporter.Id == id).OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (type == 9 && status != 9)
            {
                count = _disasterInfoTbRepository.Count(d => d.Status == status);
                result = _disasterInfoTbRepository.GetAll().Where(d => d.Status == status && d.Reporter.Id == id).OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (type != 9 && status == 9)
            {
                count = _disasterInfoTbRepository.Count(d => d.Type == type);
                result = _disasterInfoTbRepository.GetAll().Where(d => d.Type == type && d.Reporter.Id == id).OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                count = _disasterInfoTbRepository.Count(d => d.Type == type && d.Status == status);
                result = _disasterInfoTbRepository.GetAll().Where(d => d.Type == type && d.Status == status && d.Reporter.Id == id).OrderByDescending(d => d.ReportDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            int currPage = pageIndex;
            int totalPage = (int)Math.Ceiling(count / (pageSize * 1.0));

            return new RuimapPageResultDto<ReportDisasterOutput>(count, currPage, totalPage, result.MapTo<List<ReportDisasterOutput>>());
        }

        public List<DisasterKindDetailOutput> GetDisasterKindCount()
        {
            // 先找出有多少个子类灾情种类
            var existKinds = _disasterKindTbRespository.GetAll().Where(d => d.Pid.ToString() != "0").ToList();
            if(existKinds == null)
            {
                throw new UserFriendlyException("没有灾情种类");
            }

            List<DisasterKindDetailOutput> outList = new List<DisasterKindDetailOutput>();
            // 分别查找出对应的灾情个数
            foreach (var i in existKinds)
            {
                var _count = _disasterInfoTbRepository.Count(d => d.DisasterKind.Id == i.Id);
                outList.Add(new DisasterKindDetailOutput()
                {
                    KindName = i.Name,
                    KindCount = _count
                });
            }

            return outList;
        }

        public List<DisasterKindNetWorkOutput> GetDisasterNetworkCount()
        {
            // 上报类型 1-移动网络 2-北斗短报文
            List<DisasterKindNetWorkOutput> outList = new List<DisasterKindNetWorkOutput>();

            // 移动
            var _count = _disasterInfoTbRepository.Count(d => d.Type == 1);
            outList.Add(new DisasterKindNetWorkOutput()
            {
                Type = "移动上报",
                Count = _count
            });

            _count = _disasterInfoTbRepository.Count(d => d.Type == 2);
            outList.Add(new DisasterKindNetWorkOutput()
            {
                Type = "北斗上报",
                Count = _count
            });

            return outList;
        }

        public List<DisasterTrendOutput> GetDisasterTrend(int type = 1)
        {
            // 默认得到1年以内的灾情趋势
            var currDate = DateTime.Today;
            List<DisasterTrendOutput> outList = new List<DisasterTrendOutput>();
            if(type == 1)
            {
                // 获取12个月份的记录
                for (var i = 0; i < 12; i++)
                {
                    // 全部
                    var tempCurrDate = currDate.AddMonths(-i);
                    var _count = _disasterInfoTbRepository.Count(d => d.ReportDate.Month == tempCurrDate.Month && d.ReportDate.Year == tempCurrDate.Year);
                    // 已处理
                    var _solveCount = _disasterInfoTbRepository.Count(d => d.ReportDate.Month == tempCurrDate.Month && d.ReportDate.Year == tempCurrDate.Year && d.Status == 2);
                    outList.Add(new DisasterTrendOutput()
                    {
                        Count = _count,
                        SolveCount = _solveCount,
                        Date = currDate.AddMonths(-i).Year.ToString() + "年" + currDate.AddMonths(-i).Month.ToString() + "月"
                    });
                }
            }
            else if(type == 2)
            {
                // 获取30天以内的记录
                for (var i = 0; i < 30; i++)
                {
                    // 全部
                    var tempCurrDate = currDate.AddDays(-i);
                    var _count = _disasterInfoTbRepository.Count(d => d.ReportDate.Year == tempCurrDate.Year && d.ReportDate.Month == tempCurrDate.Month && d.ReportDate.Day == tempCurrDate.Day);
                    // 已处理
                    var _solveCount = _disasterInfoTbRepository.Count(d => d.ReportDate.Year == tempCurrDate.Year && d.ReportDate.Month == tempCurrDate.Month && d.ReportDate.Day == tempCurrDate.Day && d.Status == 2);
                    outList.Add(new DisasterTrendOutput()
                    {
                        Count = _count,
                        SolveCount = _solveCount,
                        Date = currDate.AddDays(-i).Year.ToString() + "年" + currDate.AddDays(-i).Month.ToString() + "月" + currDate.AddDays(-i).Day.ToString() + "日"
                    });
                }
            }
            return outList;
        }

        public DisasterResultSumOutput GetDisasterResultSum()
        {
            DisasterResultSumOutput outResult = new DisasterResultSumOutput();

            outResult.DisasterCount = _disasterInfoTbRepository.Count();
            outResult.FinishCount = _disasterInfoTbRepository.Count(d => d.Status == 2);
            outResult.RemainderCount = _disasterInfoTbRepository.Count(d => d.Status != 2);
            var tempCurrDate = DateTime.Today;
            outResult.TodayCount = _disasterInfoTbRepository.Count(d => d.ReportDate.Year == tempCurrDate.Year && d.ReportDate.Month == tempCurrDate.Month && d.ReportDate.Day == tempCurrDate.Day);

            outResult.RescueCount = _messageGroupRespository.Count();
            outResult.ReporterCount = _reporterInfoRespository.Count(r => r.Type == 1);

            return outResult;
        }

        public ReportDisasterOutput SetDisasterStatus(SetDisasterStatusInput input)
        {
            var existDisaster = _disasterInfoTbRepository.FirstOrDefault(d => d.Id == input.DisasterId);
            if(existDisaster == null)
            {
                throw new UserFriendlyException("没有相应的灾情");
            }

            existDisaster.Status = input.Status;

            _disasterInfoTbRepository.InsertOrUpdate(existDisaster);

            return existDisaster.MapTo<ReportDisasterOutput>();
        }

        public List<DisasterAreaTotalOutput> GetAreaTotal()
        {
            var result = _disasterInfoTbRepository.GetAll().GroupBy(d => d.AreaCode).ToList();
            var outResult = new List<DisasterAreaTotalOutput>();
            for(var i = 0; i < result.Count; i++)
            {
                var key = result[i].Key;
                var count = _disasterInfoTbRepository.Count(d => d.AreaCode == key);
                var existDisaster = _disasterInfoTbRepository.FirstOrDefault(d => d.AreaCode == key);
                outResult.Add(new DisasterAreaTotalOutput()
                {
                    Count = count,
                    Lat = existDisaster.Lat,
                    Lng = existDisaster.Lng,
                    Name = existDisaster.DisasterAddress
                });
            }
            return outResult;
        }
    }
}
