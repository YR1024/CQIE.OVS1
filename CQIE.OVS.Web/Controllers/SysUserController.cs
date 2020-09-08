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
    public class SysUserController : Controller
    {
        #region 列表
        public ActionResult Index(int pageIndex = 1, string qryUserName = "")
        {
            //满足条件记录总数
            int count = 0;
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<SysUser> list = Container.Instance.Resolve<ISysUserService>().Query(new List<ICriterion>() { new LikeExpression("Name", "%" + qryUserName + "%") }, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合
            PageList<SysUser> pageList = list.ToPageList<SysUser>(
                pageIndex
                , PagerHelper.PageSize
                , count);
            return View(pageList);//用pageList集合填充页面。
        }
        #endregion

        #region 登录
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>是否成功；0用户名密码错误</returns>
        [HttpPost]
        public ActionResult Login(string account, string password = "123")
        {

            password = AppHelper.EncodeMd5(password);
            SysUser loginUser = Container.Instance.Resolve<ISysUserService>().Login(account, password);
            
            if (loginUser != null)
            {
                AppHelper.LoginedUser = loginUser;//保存用户登录信息
                //return AppHelper.Host + "main.htm";//将要跳转的页面路径返回视图http://localhost:2533/User/LoginIndex
                if (loginUser.RoleList[0].Name == "观众" && loginUser.RoleList.Count() == 1)//判断是否为观众登录
                {
                    return RedirectToAction("Index","Vote");
                }
                return Redirect(AppHelper.Host+"main.htm");
                //return Redirect("main.htm");  //不会经过控制器
            }
            return View();//代表用户户或密码错误


        }
        #endregion

        #region 新增
        public ActionResult Create()
        {
            SysUser user = new SysUser();
            //获取所有角色
            IList<SysRole> lst = Container.Instance.Resolve<ISysRoleService>().GetAll().Where(o=>o.IsActive==1).ToList();
            IList<SelectListItem> lstRole = new List<SelectListItem>();
            //构建角色多选框的内容；
            foreach (var r in lst)
            {
                SelectListItem sli = new SelectListItem() { Text = r.Name, Value = r.ID.ToString(), Selected = false };
                lstRole.Add(sli);
            }
            ViewBag.lstRole = lstRole;
            //构建状态信息
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="激活",Selected=true},
                 new SelectListItem(){Value="0",Text="禁用",Selected=false}
            };
            ViewBag.lstStatus = lstStatus;
            return View(user);

        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="roleIDs">角色IDS</param>
        /// <param name="statusValue">用户状态</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(SysUser user, string roleIDs = "", string statusValue = "0")
        {
            try
            {
                //拆分角色字符串；
                string[] arrIds = roleIDs.Split(',');
                IList<SysRole> roleList = new List<SysRole>();
                foreach (string n in arrIds)
                {
                    if (n != null && n != "")
                    {
                        //根据ID查找角色信息
                        roleList.Add(Container.Instance.Resolve<ISysRoleService>().GetEntity(Int32.Parse(n)));
                    }
                }
                //构建实体
                user.RoleList = roleList;
                user.Password = AppHelper.EncodeMd5(user.Password);
                user.IsActive = Int32.Parse(statusValue);
                //保存到数据库中
                Container.Instance.Resolve<ISysUserService>().Add(user);
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
            SysUser user = Container.Instance.Resolve<ISysUserService>().GetEntity(id);
            if(user.IsActive==1)
            {
                user.IsActive = 0;  //改为禁用
            }
            else
            {
                user.IsActive = 1;  //改为激活
            }
            Container.Instance.Resolve<ISysUserService>().Update(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region 编辑
        public ActionResult Edit(int id)
        {
            SysUser user = Container.Instance.Resolve<ISysUserService>().GetEntity(id);
            //获取所有角色
            IList<SysRole> list = Container.Instance.Resolve<ISysRoleService>().GetAll().Where(o => o.IsActive == 1).ToList();
            IList<SelectListItem> lstRole = new List<SelectListItem>();
            bool isactive = false;
            bool isHas = false;
            //构建角色多选框的内容；
            foreach (var r in list)
            {
                foreach(var ur in user.RoleList )
                {
                    if (r.Name==ur.Name)
                    {
                        isHas = true;
                        break;
                    }
                }
                SelectListItem sli = new SelectListItem() { Text = r.Name, Value = r.ID.ToString(), Selected = isHas };
                isHas = false;
                lstRole.Add(sli);
            }

            ViewBag.lstRole = lstRole;
            //构建状态信息
            if(user.IsActive==1)
            {
                isactive = true;
            }
            IList<SelectListItem> lstStatus = new List<SelectListItem>()
            {
                 new SelectListItem(){Value="1",Text="激活",Selected=isactive},
                 new SelectListItem(){Value="0",Text="禁用",Selected=!isactive}
            };
            ViewBag.lstStatus = lstStatus;
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(SysUser user, string roleIDs = "", string statusValue = "0")
        {

                //拆分角色字符串；
                string[] arrIds = roleIDs.Split(',');
                IList<SysRole> roleList = new List<SysRole>();
                foreach (string n in arrIds)
                {
                    if (n != null && n != "")
                    {
                        //根据ID查找角色信息
                        roleList.Add(Container.Instance.Resolve<ISysRoleService>().GetEntity(Int32.Parse(n)));
                    }
                }
                //构建实体
                user.RoleList = roleList;
                user.Password = AppHelper.EncodeMd5(user.Password);
                user.IsActive = Int32.Parse(statusValue);
                //保存到数据库中
                Container.Instance.Resolve<ISysUserService>().Update(user);
                return RedirectToAction("Index");
          

        }
        #endregion

    }
}