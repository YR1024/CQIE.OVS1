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
    public class SysRoleController : Controller
    {
        
        #region 角色列表
        public ActionResult Index(int pageIndex = 1, string qryRoleName = "")
        {
            //满足条件记录总数
            int count = 0;
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<SysRole> list = Container.Instance.Resolve<ISysRoleService>().Query(new List<ICriterion>() { new LikeExpression("Name", "%" + qryRoleName + "%") }, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合
            PageList<SysRole> pageList = list.ToPageList<SysRole>(
                pageIndex
                , PagerHelper.PageSize
                , count);
            return View(pageList);//用pageList集合填充页面。
        }
        #endregion


        #region 新增角色
        public ActionResult Create()
        {
            SysRole role = new SysRole();
            //获取所有菜单
            IList<SystemFunction> lst = Container.Instance.Resolve<ISystemFunctionService>().GetAll().ToList();
            IList<SelectListItem> lstFun = new List<SelectListItem>();
            //构建菜单多选框的内容；
            foreach (var r in lst)
            {
                SelectListItem sli = new SelectListItem() { Text = r.SystemFunctionName, Value = r.ID.ToString(), Selected = false };
                lstFun.Add(sli);
            }
            ViewBag.lstFun = lstFun;
            //构建状态信息
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="激活",Selected=true},
                 new SelectListItem(){Value="0",Text="禁用",Selected=false}
            };
            ViewBag.lstStatus = lstStatus;
            return View(role);

        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="roleIDs">角色IDS</param>
        /// <param name="statusValue">用户状态</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(SysRole role, string FunIDs = "", string statusValue = "0")
        {
            try
            {
                //拆分角色字符串；
                string[] arrIds = FunIDs.Split(',');
                IList<SystemFunction> funList = new List<SystemFunction>();
                foreach (string n in arrIds)
                {
                    if (n != null && n != "")
                    {
                        //根据ID查找角色信息
                        funList.Add(Container.Instance.Resolve<ISystemFunctionService>().GetEntity(Int32.Parse(n)));
                    }
                }
                //构建实体
                role.FunList = funList;
                role.IsActive = Int32.Parse(statusValue);
                //保存到数据库中
                Container.Instance.Resolve<ISysRoleService>().Add(role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region 修改状态
        public ActionResult ChangeStatus(int id)
        {
            SysRole role = Container.Instance.Resolve<ISysRoleService>().GetEntity(id);
            if (role.IsActive == 1)
            {
                role.IsActive = 0;  //改为禁用
            }
            else
            {
                role.IsActive = 1;  //改为激活
            }
            Container.Instance.Resolve<ISysRoleService>().Update(role);
            return RedirectToAction("Index");
        }
        #endregion

        #region 编辑
        public ActionResult Edit(int id)
        {
            SysRole role = Container.Instance.Resolve<ISysRoleService>().GetEntity(id);
            //获取所有功能
            IList<SystemFunction> list = Container.Instance.Resolve<ISystemFunctionService>().GetAll().ToList();
            IList<SelectListItem> listFun = new List<SelectListItem>();
            bool isactive = false;
            bool isHas = false;
            //构建功能多选框的内容；
            foreach (var f in list)
            {
                foreach (var ur in role.FunList)
                {
                    if (f.SystemFunctionName == ur.SystemFunctionName)
                    {
                        isHas = true;
                        break;
                    }
                }
                SelectListItem sli = new SelectListItem() { Text = f.SystemFunctionName, Value = f.ID.ToString(), Selected = isHas };
                isHas = false;
                listFun.Add(sli);
            }

            ViewBag.listFun = listFun;
            //构建状态信息
            if (role.IsActive == 1)
            {
                isactive = true;
            }
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="激活",Selected=isactive},
                 new SelectListItem(){Value="0",Text="禁用",Selected=!isactive}
            };
            ViewBag.lstStatus = lstStatus;
            return View(role);
        }
        [HttpPost]
        public ActionResult Edit(SysRole role, string roleIDs = "", string statusValue = "0")
        {
            try
            {
                //拆分角色字符串；
                string[] arrIds = roleIDs.Split(',');
                IList<SystemFunction> funList = new List<SystemFunction>();
                foreach (string n in arrIds)
                {
                    if (n != null && n != "")
                    {
                        //根据ID查找角色信息
                        funList.Add(Container.Instance.Resolve<ISystemFunctionService>().GetEntity(Int32.Parse(n)));
                    }
                }
                //构建实体
                role.FunList = funList;
                role.IsActive = Int32.Parse(statusValue);
                //保存到数据库中
                Container.Instance.Resolve<ISysRoleService>().Update(role);
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