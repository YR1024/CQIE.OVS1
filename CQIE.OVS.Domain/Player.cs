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
    public class Player:BaseEntity<Player>
    {
        /// <summary>
        /// 选手姓名
        /// </summary>
        [Property(Length = 30, NotNull = true)]
        [Display(Name ="选手姓名")]
        [Required(ErrorMessage = "请填写选手姓名！")]
        [StringLength(30, ErrorMessage = "选手姓名不超过30位！")]
        public string Name
        { get; set; }

        /// <summary>
        /// 代表作
        /// </summary>
        [Property(Length = 30, NotNull = true)]
        [Display(Name = "代表作")]
        [Required(ErrorMessage = "请填写代表作！")]
        [StringLength(30, ErrorMessage = "代表作名称不超过30位！")]
        public string MainWork
        { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Property(Length = 300, NotNull = true)]
        [Display(Name = "图片路径")]
        [Required(ErrorMessage = "请填写图片路径！")]
        [StringLength(300, ErrorMessage = "图片路径长度不超过300位！")]
        public string PictureUrl
        { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        /// 1代表男性，0代表女性
        [Property(NotNull = true)]
        [Display(Name = "性别")]
        [Required(ErrorMessage = "请填写性别！")]
        public int Sex
        { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Property(Length = 11, NotNull = true)]
        [Display(Name = "联系电话")]
        [Required(ErrorMessage ="请填写电话号码！")]
        [StringLength(11, ErrorMessage = "电话号码长度不超过11位！")]
        [DataType(DataType.PhoneNumber)]
        public string Tel
        { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Property(Length = 4000, NotNull = true)]
        [Display(Name = "备注")]
        //[Required(ErrorMessage = "请填写描述！")]
        [StringLength(4000, ErrorMessage = "描述内容不能超过4000位！")]
        [DataType(DataType.MultilineText)]
        public string Remarke
        { get; set; }
    }
}  
