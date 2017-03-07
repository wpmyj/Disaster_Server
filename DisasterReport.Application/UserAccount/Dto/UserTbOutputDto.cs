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
    [AutoMap(typeof(UserTb))]
    public class UserTbOutputDto:EntityDto<Guid>
    {
        /// <summary>
        /// 用户登陆账号
        /// </summary>
        public virtual string UserCode { get; set; }
        /// <summary>
        /// 账号是否已经启用
        /// </summary>
        public virtual Boolean Enable { get; set; }
    }
}
