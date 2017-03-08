using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class DisasterTrendOutput
    {
        /// <summary>
        /// 日期
        /// </summary>
        public virtual string Date { get; set; }
        /// <summary>
        /// 上报数
        /// </summary>
        public virtual int Count { get; set; }
        /// <summary>
        /// 解决总数
        /// </summary>
        public virtual int SolveCount { get; set; }
    }
}
