using Abp.Application.Services;
using DisasterReport.CityService.Dto;
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
    }
}
