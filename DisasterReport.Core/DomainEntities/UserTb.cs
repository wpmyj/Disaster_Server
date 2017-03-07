using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("dr_UserTb")] //用户管理表
    public class UserTb : Entity<Guid>
    {
        /// <summary>
        /// 用户登陆账号
        /// </summary>
        public virtual string UserCode { get; set; }
        /// <summary>
        /// 用户登陆密码
        /// </summary>
        public virtual String Password { get; set; }
        /// <summary>
        /// 账号是否已经启用
        /// </summary>
        public virtual Boolean Enable { get; set; }
    }
}
