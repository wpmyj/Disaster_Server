﻿using Abp.Application.Services;
using DisasterReport.DtoTemplate;
using DisasterReport.ReporterService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DisasterReport.ReporterService
{
    /// <summary>
    /// 上报人员接口
    /// </summary>
    public interface IReporterAppService: IApplicationService
    {
        /// <summary>
        /// 新增上报人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        Task<ReporterOutput> AddReporter(ReporterAddInput input);
        /// <summary>
        /// 分页得到上报人员
        /// </summary>
        /// <param name="type">默认9 所有类型 1-上报人员 2-后台管理者</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">默认9999</param>
        /// <param name="hasDevice">默认9 全部 1绑定了设备 2 没有绑定</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<ReporterOutput> GetPageReporter(int type = 9, int pageIndex = 1, int pageSize = 9999, int hasDevice = 9);
        /// <summary>
        /// 根据人员id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        Task<ReporterOutput> GetReporterById(Guid id);
        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ReporterOutput UpdateReporter(ReporterUpdateInput input);
        /// <summary>
        /// 上报人员按姓名、手机模糊查找
        /// </summary>
        /// <param name="nameOrPhone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<ReporterOutput> GetReporterByNameOrPhone(string nameOrPhone, int pageIndex = 1, int pageSize = 9999);
        /// <summary>
        /// 更新上报人员位置
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        void UpdateReporterLastPos(UpdateReporterLastPosInput input);
    }
}
