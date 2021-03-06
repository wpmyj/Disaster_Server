﻿using Abp.Application.Services;
using DisasterReport.DisasterService.Dto;
using DisasterReport.DtoTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DisasterReport.DisasterService
{
    /// <summary>
    /// 灾情管理接口
    /// </summary>
    public interface IDisasterAppService : IApplicationService
    {
        /// <summary>
        /// 手持终端上报灾情
        /// </summary>
        [HttpPost]
        void ReportDisaster(ReportDisasterInput input);
        /// <summary>
        /// 分页获取灾情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">默认9999</param>
        /// <param name="type">上报类型 1-移动网络 2-北斗短报文 9-全部</param>
        /// <param name="status">灾情是否已处理 0-没有处理 1-正在处理 2-已处理 9-全部</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<ReportDisasterOutput> GetPageDisaster(int pageIndex = 1, int pageSize = 9999, int type = 9, int status = 9);
        /// <summary>
        /// 根据灾情Id查询灾情详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        Task<ReportDisasterOutput> GetDisasterById(Guid id);
        /// <summary>
        /// 获取灾情图片信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        List<UploadsFileTbOutput> GetDisasterFilePicById(Guid id);
        /// <summary>
        /// 分页获取指定用户最近上报的灾情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex">默认 1</param>
        /// <param name="pageSize">默认 9999</param>
        /// <param name="type">默认 9</param>
        /// <param name="status">默认 9</param>
        /// <returns></returns>
        [HttpGet]
        RuimapPageResultDto<ReportDisasterOutput> GetPageDisasterByReporterId(Guid id, int pageIndex = 1, int pageSize = 9999, int type = 9, int status = 9);
        /// <summary>
        /// 统计各类灾情上报个数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        List<DisasterKindDetailOutput> GetDisasterKindCount();
        /// <summary>
        /// 移动端和北斗端的上报数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        List<DisasterKindNetWorkOutput> GetDisasterNetworkCount();
        /// <summary>
        /// 得到最近的灾情趋势结果
        /// </summary>
        /// <param name="type">1-（1年以内） 2-（1月以内30天）</param>
        /// <returns></returns>
        [HttpGet]
        List<DisasterTrendOutput> GetDisasterTrend(int type = 1);
        /// <summary>
        /// 统计灾情总数、已销结数、未解决数、今日上报数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        DisasterResultSumOutput GetDisasterResultSum();
        /// <summary>
        /// 灾情状态的流转
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        ReportDisasterOutput SetDisasterStatus(SetDisasterStatusInput input);
        /// <summary>
        /// 得到灾情的区域统计总数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        List<DisasterAreaTotalOutput> GetAreaTotal();
        /// <summary>
        /// 得到子级灾情种类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        List<DisasterKindOutput> GetDisasterKind();
        /// <summary>
        /// 修改灾情种类图标
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        void UpdateDisasterKindIcon(DisasterKindUpdateInput input);
        /// <summary>
        /// 响应灾情救援
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        void ResponseDisaster(ResponseDisasterInput input);
        /// <summary>
        /// 判断是否已经加入过此灾情救援行动中
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        void HasJoinDisasterRescue(HasJoinDisasterRescueInput input);
    }
}
