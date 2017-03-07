using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.UserAccount.Dto
{
    public class UserAccountAddUserInput
    {
        /// <summary>
        /// 用户登陆账号
        /// </summary>
        public virtual string UserCode { get; set; }
        /// <summary>
        /// 用户登陆密码
        /// </summary>
        public virtual String Password { get; set; }
    }
}
