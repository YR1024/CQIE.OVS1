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
    public class MatchInfo : BaseEntity<MatchInfo>
    {
        /// <summary>
        /// 比赛名称
        /// </summary>
        [Display(Name = "比赛名称")]
        [Property(Length = 300, NotNull = false)]
        [Required(ErrorMessage = "请填写比赛名称！")]
        [StringLength(300, ErrorMessage = "比赛名称不超过300位！")]
        public string Name { get; set; }

        /// <summary>
        /// 比赛地址
        /// </summary>
        [Display(Name = "比赛地址")]
        [Property(Length = 300, NotNull = false)]
        [Required(ErrorMessage = "请填写比赛的地址！")]
        [StringLength(300, ErrorMessage = "比赛地址的长度不超过300位！")]
        public string Address { get; set; }

        /// <summary>
        /// 主办方
        /// </summary>
        [Display(Name = "主办方")]
        [Property(Length = 300, NotNull = false)]
        [Required(ErrorMessage = "请填写主办方！")]
        [StringLength(300, ErrorMessage = "主办方名称不超过300位！")]
        public string Sponsor { get; set; }

        /// <summary>
        /// 比赛时间
        /// </summary>
        [Display(Name = "比赛时间")]
        [Property(NotNull = true)]
        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [Property(Length = 30, NotNull = false)]
        [Required(ErrorMessage = "请填写联系人！")]
        [StringLength(30, ErrorMessage = "联系人的长度不超过30位！")]
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        [Property(Length = 11, NotNull = false)]
        [Required(ErrorMessage = "请填写电话号码！")]
        [StringLength(11, ErrorMessage = "电话号码长度不超过11位！")]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }

        /// <summary>
        /// 包含对战信息列表；
        /// </summary>
        [Display(Name = "包含对战信息列表")]
        [HasMany(typeof(PKInfo), Table = "PKInfo", ColumnKey = "MatchInfoId", Lazy = false, Cascade = ManyRelationCascadeEnum.None,Inverse =true)]       
        public IList<PKInfo> PKInfoList { get; set; }
    }
}
