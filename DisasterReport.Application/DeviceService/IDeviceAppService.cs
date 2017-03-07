using Abp.Application.Services;
using DisasterReport.DeviceService.Dto;
using DisasterReport.DtoTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DisasterReport.DeviceService
{
    /// <summary>
    /// 设备接口服务
    /// </summary>
    public interface IDeviceAppService: IApplicationService
    {
        /// <summary>
        /// 查询用户所绑定的设备
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        Task<DeviceOutput> GetDeviceByReporterId(Guid id);
        /// <summary>
        /// 设备绑定到指定的上报人员
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        void BindReporter(DeviceBindInput input);
        /// <summary>
        /// 分页得到终端设备
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">默认10</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<DeviceOutput> GetPageDevice(int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 新增手持终端设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        Task<DeviceOutput> AddDevice(DeviceAddInput input);
    }
}
