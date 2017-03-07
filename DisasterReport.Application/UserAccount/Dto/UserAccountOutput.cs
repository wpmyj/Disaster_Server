using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DisasterReport.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.UserAccount.Dto
{
    [AutoMap(typeof(ReporterInfoTb))]
    public class UserAccountOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual String Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int Age { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual String Phone { get; set; }
        /// <summary>
        /// 所属区域编码
        /// </summary>
        public virtual String AreaCode { get; set; }
        /// <summary>
        /// 所在地址
        /// </summary>
        public virtual String Address { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public virtual string Photo { get; set; }
        /// <summary>
        /// 1）上报人员 2）后台管理人员  两类节点
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 人员备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 用户账号信息
        /// </summary>
        public virtual UserTbOutputDto User { get; set; }
    }
}
