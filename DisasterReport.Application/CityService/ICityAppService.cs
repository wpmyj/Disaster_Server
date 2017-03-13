using Abp.Application.Services;
using DisasterReport.CityService.Dto;
using DisasterReport.DtoTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DisasterReport.CityService
{
    /// <summary>
    /// 城市查询服务
    /// </summary>
    public interface ICityAppService : IApplicationService
    {
        /// <summary>
        /// 根据社区名称返回信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        CommunityOutput GetCommunityInfoByName(string name);
        /// <summary>
        /// 根据行政区域类型获取信息
        /// </summary>
        /// <param name="type">1 2 3 4 省 市 区 街道</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">默认20</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<CityOutput> GetPageCity(int type, int pageIndex = 1, int pageSize = 20);
        /// <summary>
        /// 根据父级行政区域获取信息
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">默认20</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<CityOutput> GetCityByPid(Int64 pId, int pageIndex = 1, int pageSize = 20);
    }
}
