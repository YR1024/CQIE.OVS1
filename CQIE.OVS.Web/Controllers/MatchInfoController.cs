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
    public class MatchInfoController : Controller
    {
        #region 比赛列表
        public ActionResult Index(int pageIndex = 1, string qryMatchName = "")
        {
            //满足条件记录总数
            int count = 0;
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<MatchInfo> list = Container.Instance.Resolve<IMatchInfoService>().Query(new List<ICriterion>() { new LikeExpression("Name", "%" + qryMatchName + "%") }, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合
            PageList<MatchInfo> pageList = list.ToPageList<MatchInfo>(
                pageIndex
                , PagerHelper.PageSize
                , count);
            return View(pageList);//用pageList集合填充页面。
        }
        #endregion

        #region 新增比赛
        public ActionResult Create()
        {
            MatchInfo match = new MatchInfo();

            ////获取所有角色
            //IList<SysRole> lst = Container.Instance.Resolve<ISysRoleService>().GetAll().Where(o => o.IsActive == 1).ToList();
            //IList<SelectListItem> lstRole = new List<SelectListItem>();
            ////构建角色多选框的内容；
            //foreach (var r in lst)
            //{
            //    SelectListItem sli = new SelectListItem() { Text = r.Name, Value = r.ID.ToString(), Selected = false };
            //    lstRole.Add(sli);
            //}
            //ViewBag.lstRole = lstRole;

            ////构建状态信息
            //IList<SelectListItem> lstStatus = new List<SelectListItem>()
            //{
            //     new SelectListItem(){Value="1",Text="激活",Selected=true},
            //     new SelectListItem(){Value="2",Text="禁用",Selected=false}
            //};
            //ViewBag.lstStatus = lstStatus;

            return View(match);

        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="roleIDs">角色IDS</param>
        /// <param name="statusValue">用户状态</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(MatchInfo match)
        {
            try
            {
                //构建实体
                //保存到数据库中
                Container.Instance.Resolve<IMatchInfoService>().Add(match);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region 对战列表
        public ActionResult PkInfo(int id,int pageIndex = 1)
        {
            MatchInfo match= Container.Instance.Resolve<IMatchInfoService>().GetEntity(id);
            //满足条件记录总数
            int count = 0;
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<PKInfo> list = Container.Instance.Resolve<IPKInfoService>().GetAll().ToList();
            List<PKInfo> list1 = new List<PKInfo>();
            foreach (var a in list)
            {
                if (a.MatchInfo == match)
                {
                    list1.Add(a);
                }
            }
            //转换为PageList集合
            PageList<PKInfo> pageList = list1.ToPageList<PKInfo>(
                pageIndex
                , PagerHelper.PageSize
                , count);
            return View(pageList);//用pageList集合填充页面。
        }
        #endregion

        #region 创建对战
        public ActionResult CreatePk()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreatePk(PKInfo pkinfo)
        {
            
            return View();
        }
        #endregion

        #region 编辑
        public ActionResult Edit(int id)
        {
            MatchInfo match = Container.Instance.Resolve<IMatchInfoService>().GetEntity(id);
            ViewBag.date=match.MatchDate.ToShortDateString().ToString();
            return View(match);
        }
        [HttpPost]
        public ActionResult Edit(MatchInfo match)
        {
            Container.Instance.Resolve<IMatchInfoService>().Update(match);
            return RedirectToAction("Index");
        }
        #endregion
    }
}