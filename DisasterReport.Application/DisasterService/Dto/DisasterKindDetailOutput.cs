using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class DisasterKindDetailOutput
    {
        /// <summary>
        /// 灾情种类
        /// </summary>
        public virtual string KindName { get; set; }
        /// <summary>
        /// 灾情总数
        /// </summary>
        public virtual int KindCount { get; set; }
    }
}
