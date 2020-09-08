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
    public class PlayerController : Controller
    {
        #region 选手列表
        public ActionResult Index(int pageIndex = 1, string qryPlayerName = "")
        {
            //满足条件记录总数
            int count = 0;
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<Player> list = Container.Instance.Resolve<IPlayerService>().Query(new List<ICriterion>() { new LikeExpression("Name", "%" + qryPlayerName + "%") }, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合
            PageList<Player> pageList = list.ToPageList<Player>(
                pageIndex
                , PagerHelper.PageSize
                , count);
            return View(pageList);//用pageList集合填充页面。
        }
        #endregion

        #region 新增选手
        public ActionResult Create()
        {
            Player player = new Player();
            //构建状态信息
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="男",Selected=true},
                 new SelectListItem(){Value="0",Text="女",Selected=false}
            };
            ViewBag.lstStatus = lstStatus;
            return View(player);

        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="roleIDs">角色IDS</param>
        /// <param name="statusValue">用户状态</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Player player,string statusValue = "1",string file="")
        {
            try
            {
                //构建实体
                player.PictureUrl = "../../Content/playerImage/" + file;
                player.Sex = Int32.Parse(statusValue);
                //保存到数据库中
                Container.Instance.Resolve<IPlayerService>().Add(player);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region
        public ActionResult Edit(int id)
        {
            Player player = Container.Instance.Resolve<IPlayerService>().GetEntity(id);
            bool isactive = false;
            if (player.Sex == 1)
            {
                isactive = true;
            }
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="男",Selected=isactive},
                 new SelectListItem(){Value="0",Text="女",Selected=!isactive}
            };
            ViewBag.lstStatus = lstStatus;
            
            return View(player);
        }

        [HttpPost]
        public ActionResult Edit(Player player,string statusValue = "1",string file = "")
        {
            try
            {
                //构建实体
                if(file!="")
                {
                    player.PictureUrl= "../../Content/playerImage/" + file;
                }
                player.Sex = Int32.Parse(statusValue);
                //保存到数据库中
                Container.Instance.Resolve<IPlayerService>().Update(player);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}