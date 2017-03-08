using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class SetDisasterStatusInput
    {
        /// <summary>
        /// 灾情Id
        /// </summary>
        public virtual Guid DisasterId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
    }
}
