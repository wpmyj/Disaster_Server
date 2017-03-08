using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.CityService.Dto
{
    public class CityOutput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public virtual Int64 Id { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public virtual int Type { get; set; }
    }
}
