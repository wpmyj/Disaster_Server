using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_UploadsFileTb")] //上报存储的文件信息表
    public class UploadsFileTb:Entity<Guid>
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
