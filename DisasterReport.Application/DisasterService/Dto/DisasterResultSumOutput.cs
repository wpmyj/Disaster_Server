using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DisasterService.Dto
{
    public class DisasterResultSumOutput
    {
        /// <summary>
        /// 灾情总数
        /// </summary>
        public virtual int DisasterCount { get; set; }
        /// <summary>
        /// 解决总数
        /// </summary>
        public virtual int FinishCount { get; set; }
        /// <summary>
        /// 剩余总数
        /// </summary>
        public virtual int RemainderCount { get; set; }
        /// <summary>
        /// 今日总数
        /// </summary>
        public virtual int TodayCount { get; set; }
        /// <summary>
        /// 救援队总人数
        /// </summary>
        public virtual int RescueCount { get; set; }
        /// <summary>
        /// 上报人员总数
        /// </summary>
        public virtual int ReporterCount { get; set; }
    }
}
