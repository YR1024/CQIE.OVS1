using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace CQIE.OVS.Domain
{
    [ActiveRecord]
    public class SysUser:BaseEntity<SysUser>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name="用户名")]
        [Property(Length =30,NotNull =true)]
        [Required(ErrorMessage = "请填写用户名！")]
        [StringLength(30, ErrorMessage = "用户名不超过30位！")]
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号")]
        [Property(Length = 30, NotNull = true)]
        [Required(ErrorMessage = "请填写账号！")]
        [StringLength(30, ErrorMessage = "账号的长度不超过30位！")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Property(Length = 50, NotNull = true)]
        [Required(ErrorMessage = "请填写密码！")]
        [StringLength(30, ErrorMessage = "密码的长度不超过30位！")]
        public string Password { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        /// /// 1代表激活，0代表禁用
        [Display(Name = "用户状态")]
        [Property(NotNull = true)]
        public int IsActive { get; set; }

        /// <summary>
        /// 这个用户所拥有的角色权限
        /// </summary>
        [HasAndBelongsToMany(typeof(SysRole),Table ="SysUser_SysRole",ColumnKey ="SysUserID",ColumnRef ="SysRoleID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "所属角色")]
        public IList<SysRole>  RoleList { get; set; }
    }
}
