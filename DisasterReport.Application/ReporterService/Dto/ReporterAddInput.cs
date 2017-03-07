using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.ReporterService.Dto
{
    public class ReporterAddInput
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual String Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual String Phone { get; set; }
        /// <summary>
        /// 所属区域编码
        /// </summary>
        public virtual String AreaCode { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int Age { get; set; }
        /// <summary>
        /// 所在地址
        /// </summary>
        public virtual String Address { get; set; }
        /// <summary>
        /// 上报人员所关联的用户Id
        /// </summary>
        public virtual Guid UserId { get; set; }
        /// <summary>
        /// 1）上报人员 2）后台管理人员  两类节点
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 人员备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
