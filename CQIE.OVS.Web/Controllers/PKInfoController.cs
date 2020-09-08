using CQIE.OVS.Core;
using CQIE.OVS.Domain;
using CQIE.OVS.Service;
using CQIE.OVS.Web.Apps;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQIE.OVS.Web.Controllers
{
    public class PKInfoController : Controller
    {
        // GET: PKInfo
        #region 对战列表
        public ActionResult Index(int pageIndex = 1, string MatchInfoId = "")
        {           
            //满足条件记录总数
            int count = 0;
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<PKInfo> pklist = null;
            if (MatchInfoId=="")
            {
                pklist = Container.Instance.Resolve<IPKInfoService>().GetAll().ToList();
            }
            else
            {
                MatchInfo matchinfo = Container.Instance.Resolve<IMatchInfoService>().GetEntity(int.Parse(MatchInfoId));
                pklist = Container.Instance.Resolve<IPKInfoService>().Query(new List<ICriterion>() { Expression.Eq("MatchInfo", matchinfo) }, listOrder, pageIndex, PagerHelper.PageSize, out count);
            }
           
            // 转换为PageList集合
            PageList<PKInfo> pageList = pklist.ToPageList<PKInfo>(
                pageIndex
                , PagerHelper.PageSize
                , count);
            //获取比赛下拉框
            IList<MatchInfo> matchList=Container.Instance.Resolve<IMatchInfoService>().GetAll().ToList();
            IList<SelectListItem> MatchList = new List<SelectListItem>();
            foreach (var m in matchList)
            {
                MatchList.Add(new SelectListItem() { Value = m.ID.ToString(), Text =m.Name});
            }
            ViewBag.MatchList = MatchList;
            return View(pageList);
        }
        #endregion

        #region 新增对战
        public ActionResult Create()
        {
            //获取比赛下拉框
            IList<MatchInfo> matchList = Container.Instance.Resolve<IMatchInfoService>().GetAll().ToList();
            IList<SelectListItem> MatchList = new List<SelectListItem>();
            foreach (var m in matchList)
            {
                MatchList.Add(new SelectListItem() { Value = m.ID.ToString(), Text = m.Name });
            }
            ViewBag.MatchList = MatchList;

            //获取选手下拉框
            IList<Player> playerlist = Container.Instance.Resolve<IPlayerService>().GetAll().ToList();
            IList<SelectListItem> PlayerList = new List<SelectListItem>();
            foreach (var p in playerlist)
            {
                PlayerList.Add(new SelectListItem() { Value = p.ID.ToString(), Text = p.Name });
            }
            ViewBag.PlayerList = PlayerList;

            //构建状态信息
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="开启对战",Selected=true},
                 new SelectListItem(){Value="0",Text="不开启对战",Selected=false}
            };
            ViewBag.lstStatus = lstStatus;
            return View();
        }
        [HttpPost]
        public ActionResult Create(PKInfo pkinfo,int  PlayerOneId, int PlayerTwoId, int MatchInfoId, string statusValue = "0")
        {
            Player playerone = Container.Instance.Resolve<IPlayerService>().GetEntity(PlayerOneId);
            Player playertwo = Container.Instance.Resolve<IPlayerService>().GetEntity(PlayerTwoId);
            MatchInfo matchinfo = Container.Instance.Resolve<IMatchInfoService>().GetEntity(MatchInfoId);
            pkinfo.PlayerOne = playerone;
            pkinfo.PlayerTwo = playertwo;
            pkinfo.MatchInfo = matchinfo;
            pkinfo.IsActive = Int32.Parse(statusValue);
            Container.Instance.Resolve<IPKInfoService>().Add(pkinfo);
            return RedirectToAction("Index");
        }
        #endregion

        #region 修改比赛状态
        public ActionResult ChangeStatus(int id)
        {
            PKInfo pkinfo = Container.Instance.Resolve<IPKInfoService>().GetEntity(id);
            if(pkinfo.IsActive==1)
            {
                pkinfo.IsActive = 0;
            }
            else
            {
                pkinfo.IsActive = 1;
            }
            Container.Instance.Resolve<IPKInfoService>().Update(pkinfo);
            return RedirectToAction("Index");
        }
        #endregion

        #region 编辑对战信息
        public ActionResult Edit(int id)
        {
            PKInfo pkinfo = Container.Instance.Resolve<IPKInfoService>().GetEntity(id);
            //获取比赛下拉框
            IList<MatchInfo> matchList = Container.Instance.Resolve<IMatchInfoService>().GetAll().ToList();
            IList<SelectListItem> MatchList = new List<SelectListItem>();
            foreach (var m in matchList)
            {
                MatchList.Add(new SelectListItem() { Value = m.ID.ToString(), Text = m.Name });
            }
            ViewBag.MatchList = MatchList;
            //获取比赛首选项
            ViewBag.MatchInfo = pkinfo.MatchInfo.ID;
            //获取选手下拉框
            IList<Player> playerlist = Container.Instance.Resolve<IPlayerService>().GetAll().ToList();
            IList<SelectListItem> PlayerList = new List<SelectListItem>();
            foreach (var p in playerlist)
            {
                PlayerList.Add(new SelectListItem() { Value = p.ID.ToString(), Text = p.Name });
            }
            ViewBag.PlayerList = PlayerList;
            ////获取选手首项
            ViewBag.PlayerOne = pkinfo.PlayerOne.ID;
            ViewBag.PlayerTwo = pkinfo.PlayerTwo.ID;
            //构建状态信息
            bool isactive = false;
            if (pkinfo.IsActive == 1)
            {
                isactive = true;
            }
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="对战中",Selected=isactive},
                 new SelectListItem(){Value="0",Text="未对战",Selected=!isactive}
            };
            ViewBag.lstStatus = lstStatus;
            return View(pkinfo);
        }
        [HttpPost]
        public ActionResult Edit(PKInfo pkinfo, int PlayerOneId, int PlayerTwoId, int MatchInfoId, string statusValue = "0")
        {
            Player playerone = Container.Instance.Resolve<IPlayerService>().GetEntity(PlayerOneId);
            Player playertwo = Container.Instance.Resolve<IPlayerService>().GetEntity(PlayerTwoId);
            MatchInfo matchinfo = Container.Instance.Resolve<IMatchInfoService>().GetEntity(MatchInfoId);
            pkinfo.PlayerOne = playerone;
            pkinfo.PlayerTwo = playertwo;
            pkinfo.MatchInfo = matchinfo;
            pkinfo.IsActive = Int32.Parse(statusValue);
            Container.Instance.Resolve<IPKInfoService>().Update(pkinfo);
            return RedirectToAction("Index");
        }
        #endregion
    }
}