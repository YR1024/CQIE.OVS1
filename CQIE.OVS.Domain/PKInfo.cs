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
    public class PKInfo:BaseEntity<PKInfo>
    {
        /// <summary>
        /// 选手1
        /// </summary>
        [Display(Name = "选手1")]
        [BelongsTo(Type = typeof(Player), Column = "PlayerOneId", Lazy = FetchWhen.Immediate, Cascade = CascadeEnum.None)]
        public Player PlayerOne{ get; set; }

        /// <summary>
        /// 选手1歌曲
        /// </summary>
        [Display(Name = "选手1歌曲")]
        [Property(Length = 300, NotNull = false)]
        [Required(ErrorMessage = "请填写选手1的歌曲！")]
        [StringLength(300, ErrorMessage = "歌曲名称不超过300位！")]
        public string PlayerOneSong { get; set; }

        /// <summary>
        /// 选手1票数
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "选手1票数")]
        public int PlayerOneTicket { get; set; }

        /// <summary>
        /// 选手2
        /// </summary>
        [Display(Name = "选手2")]
        [BelongsTo(Type = typeof(Player), Column = "PlayerTwoId", Lazy = FetchWhen.Immediate, Cascade = CascadeEnum.None)]
        public Player PlayerTwo { get; set; }
            
        /// <summary>
        /// 选手2歌曲
        /// </summary>
        [Display(Name = "选手2歌曲")]
        [Property(Length = 300, NotNull = false)]
        [Required(ErrorMessage = "请填写选手2的歌曲名称！")]
        [StringLength(300, ErrorMessage = "歌曲名称不超过300位！")]
        public string PlayerTwoSong  { get; set; }

        /// <summary>
        /// 选手2票数
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name ="选手2票数")]
        public int PlayerTwoTicket  { get; set; }


        /// <summary>
        /// 对战状态
        /// </summary>
        /// 1代表对战中，0代表未对战
        [Property(NotNull = true)]
        [Display(Name = "对战状态")]
        public int IsActive
        { get; set; }


        /// <summary>
        /// 所属比赛信息
        /// </summary>
        [Display(Name = "选手所属比赛信息")]
        //[BelongsTo(Type = typeof(MatchInfo), Column = "MatchInfoId", Lazy = FetchWhen.Immediate, Cascade = CascadeEnum.None)]
        [BelongsTo(Type = typeof(MatchInfo), Column = "MatchInfoId", Lazy = FetchWhen.Immediate, Cascade = CascadeEnum.None)]
        public MatchInfo MatchInfo
        { get; set; }
    }
}
