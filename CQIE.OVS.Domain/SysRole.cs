using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Castle.ActiveRecord;

namespace CQIE.OVS.Domain
{
    [ActiveRecord]
    public class SysRole : BaseEntity<SysRole>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Name = "角色名")]
        [Property(Length = 30, NotNull = true)]
        [Required(ErrorMessage = "请填写角色名称！")]
        [StringLength(30, ErrorMessage = "角色名称不超过30位！")]
        public string Name
        { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        /// 1代表激活，0代表禁用
        [Display(Name = "角色状态")]
        [Property(NotNull = true)]
        public int IsActive
        { get; set; }

        /// <summary>
        /// 当前角色下的总用户数
        /// </summary>
        [HasAndBelongsToMany(typeof(SysUser), Table = "SysUser_SysRole", ColumnKey = "SysRoleID", ColumnRef = "SysUserID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "拥有用户")]
        public IList<SysUser> Userlist
        { get; set; }

        /// <summary>
        /// 当前角色所有的菜单权限
        /// </summary>
        [HasAndBelongsToMany(typeof(SystemFunction), Table = "SystemFunction_SysRole", ColumnKey = "SysRoleID", ColumnRef = "SystemFunctionID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "菜单权限")]
        public IList<SystemFunction> FunList
        { get; set; }
    }
}
