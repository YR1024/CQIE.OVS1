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
    public class SystemFunction : BaseEntity<SystemFunction>
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = "模块名称")]
        [Property(Length = 300, NotNull = true)]
        [Required(ErrorMessage = "请填写模块名称！")]
        [StringLength(30, ErrorMessage = "模块名称不超过30位！")]
        public string SystemFunctionName { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        [Display(Name = "类名")]
        [Property(Length = 300, NotNull = false)]
        [Required(ErrorMessage = "请填写类名！")]
        [StringLength(30, ErrorMessage = "类名不超过30位！")]
        public string ClassName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [Display(Name = "控制器名称")]
        [Property(Length = 30, NotNull = false)]
        [Required(ErrorMessage = "请填写控制器名称！")]
        [StringLength(30, ErrorMessage = "控制器名称不超过30位！")]
        public string ControllerName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [Display(Name = "方法名称")]
        [Property(Length = 30, NotNull = false)]
        [Required(ErrorMessage = "请填写方法名称！")]
        [StringLength(11, ErrorMessage = "方法名称不超过30位！")]
        public string ActionName { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        [Display(Name = "排序码")]
        [Property(NotNull = true)]
        public int SortCode  { get; set; }
    
        /// <summary>
        /// 上级模块
        /// </summary>
        [BelongsTo(Type =typeof(SystemFunction),Column = "ParentID",Lazy =FetchWhen.OnInvoke)]
        [Display(Name = "上级模块")]
        public SystemFunction ParentSysFunction  { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        /// /// 1代表显示，0代表不显示
        [Display(Name = "是否显示")]
        [Property(NotNull = true)]
        public bool IsShow
        { get; set; }

        /// <summary>
        /// 拥有此菜单权限的角色
        /// </summary>
        [HasAndBelongsToMany(typeof(SysRole), Table = "SystemFunction_SysRole", ColumnKey = "SystemFunctionID", ColumnRef = "SysRoleID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "菜单权限")]
        public IList<SysRole> RoleList
        { get; set; }
      
    }
}
