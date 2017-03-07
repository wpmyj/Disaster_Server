using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DeviceService.Dto
{
    public class DeviceAddInput
    {
        /// <summary>
        /// 设备所属区域编号
        /// </summary>
        public virtual string AreaCode { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual int DeviceCode { get; set; }
    }
}
