using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_CityCodeTb")]
    public class CityCodeTb : Entity<Int64>
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public virtual Int64 Pid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual int Type {get; set; }
        /// <summary>
        /// 是否末级
        /// </summary>
        public virtual bool IsEnd { get; set; }
    }
}
