using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class DisasterKindNetWorkOutput
    {
        /// <summary>
        /// 上报类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public virtual int Count { get; set; }
    }
}
