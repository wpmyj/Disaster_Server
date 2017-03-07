using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.ReporterService.Dto
{
    public class ReporterUpdateInput
    {
        /// <summary>
        /// 人员Id
        /// </summary>
        public virtual Guid ReporterId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual String Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual String Phone { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public virtual String Photo { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int Age { get; set; }
        /// <summary>
        /// 所在地址
        /// </summary>
        public virtual String Address { get; set; }
        /// <summary>
        /// 1）上报人员 2）后台管理人员  两类节点
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 所关联的用户密码
        /// </summary>
        public virtual string Password { get; set; }
    }
}
