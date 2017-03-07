using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    [AutoMap(typeof(UploadsFileTb))]
    public class UploadsFileTbOutput:EntityDto<Guid>
    {
        /// <summary>
        /// 上报灾情信息的id值
        /// </summary>
        public virtual Guid DisasterInfoId { get; set; }

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
