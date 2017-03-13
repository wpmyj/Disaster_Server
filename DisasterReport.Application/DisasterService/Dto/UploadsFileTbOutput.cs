using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;

namespace DisasterReport.DisasterService.Dto
{
    [AutoMap(typeof(UploadsFileTb))]
    public class UploadsFileTbOutput:EntityDto<Guid>
    {
        /// <summary>
        /// 关联文件的其他rowId
        /// </summary>
        public virtual Guid OtherRowId { get; set; }

        /// <summary>
        /// 文件存储的路径
        /// </summary>
        public virtual String Path { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public virtual String FileName { get; set; }
    }
}
