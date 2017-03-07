using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterReport.DomainEntities
{
    [Table("DR_ReporterInfoTb")] //上报人员信息表
    public class ReporterInfoTb : Entity<Guid>
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
        /// 头像地址
        /// </summary>
        public virtual string Photo { get; set; }
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
        /// <summary>
        /// 最后一次上报的位置
        /// </summary>
        public virtual string LastAddress { get; set; }
        /// <summary>
        /// 最后一次上报的经度
        /// </summary>
        public virtual double LastLng { get; set; }
        /// <summary>
        /// 最后一次上报的维度
        /// </summary>
        public virtual double LastLat { get; set; }
        /// <summary>
        /// 关联的消息组
        /// </summary>
        public virtual ICollection<MessageGroupTb> MessageGroup { get; set; }
    }
}
