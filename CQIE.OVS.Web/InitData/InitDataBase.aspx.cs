using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CQIE.OVS.Domain;
using Castle.ActiveRecord.Framework;
using CQIE.OVS.Core;
using CQIE.OVS.Service;
using NHibernate.Criterion;

namespace CQIE.OVS.Web.InitData
{
    public partial class InitDataBase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInitDB_Click(object sender, EventArgs e)
        {
            string ret = "<br />";

            if (!ActiveRecordStarter.IsInitialized)
            {
                //如果ActiveRecordStarter框架没有初始化
                IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
                ActiveRecordStarter.Initialize(typeof(SysUser).Assembly, source);
            }
            ret += "正在创建数据库...<br />";
            ActiveRecordStarter.CreateSchema();
            ret += "创建数据库完成<br />";
            InitPlayer();
            InitMenu();
            InitRole();
            InitUser();
            ret += "初始化数据完成！<br/>";
            Response.Write(ret);
        }

        /// <summary>
        /// 初始化选手
        /// </summary>
        public void InitPlayer()
        {
            Container.Instance.Resolve<IPlayerService>().Add(new Player()
            {
                Name = "汪锋",
                MainWork = "怒放的生命",
                PictureUrl = "",
                Remarke = "",
                Sex = 1,
                Tel = "1592222222"
            });

            Container.Instance.Resolve<IPlayerService>().Add(new Player()
            {
                Name = "周杰伦",
                MainWork = "听妈妈的话",
                PictureUrl = "",
                Remarke = "",
                Sex = 1,
                Tel = "159333333"
            });

            Container.Instance.Resolve<IPlayerService>().Add(new Player()
            {
                Name = "王菲",
                MainWork = "因为爱情",
                PictureUrl = "",
                Remarke = "",
                Sex = 0,
                Tel = "1592222222"
            });

            Container.Instance.Resolve<IPlayerService>().Add(new Player()
            {
                Name = "蔡依林",
                MainWork = "日不落",
                PictureUrl = "",
                Remarke = "",
                Sex = 0,
                Tel = "159333333"
            });
        }

        public void InitMenu()
        {
            //id=1
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "系统管理",
                SortCode = 10,
                IsShow = true
            });
            //id=2
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "用户管理",
                ClassName = "CQIE.OVS.Web.Controllers.SysUserController",
                ControllerName = "SysUser",
                ActionName = "Index",
                SortCode = 11,
                IsShow = true,
                ParentSysFunction = Container.Instance.Resolve<ISystemFunctionService>().GetEntity(1)
            });
            //id=3
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "角色管理",
                ClassName = "CQIE.OVS.Web.Controllers.SysRoleController",
                ControllerName = "SysRole",
                ActionName = "Index",
                SortCode = 12,
                IsShow = true,
                ParentSysFunction = Container.Instance.Resolve<ISystemFunctionService>().GetEntity(1)
            });
            //id=4
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "业务管理",
                SortCode = 20,
                IsShow = true
            });
            //id=5
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "选手管理",
                ClassName = "CQIE.OVS.Web.Controllers.PlayerController",
                ControllerName = "Player",
                ActionName = "Index",
                SortCode = 21,
                IsShow = true,
                ParentSysFunction = Container.Instance.Resolve<ISystemFunctionService>().GetEntity(4)
            });

            //id=6
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "比赛管理",
                ClassName = "CQIE.OVS.Web.Controllers.MatchInfoController",
                ControllerName = "MatchInfo",
                ActionName = "Index",
                SortCode = 22,
                IsShow = true,
                ParentSysFunction = Container.Instance.Resolve<ISystemFunctionService>().GetEntity(4)
            });
            //id=7
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "对战管理",
                ClassName = "CQIE.OVS.Web.Controllers.PKInfoController",
                ControllerName = "PKInfo",
                ActionName = "Index",
                SortCode = 23,
                IsShow = true,
                ParentSysFunction = Container.Instance.Resolve<ISystemFunctionService>().GetEntity(4)
            });
            //id=8
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "观众投票",
                ClassName = "CQIE.OVS.Web.Controllers.PKInfoController",
                ControllerName = "Vote",
                ActionName = "Index",
                SortCode = 24,
                IsShow = true,
                ParentSysFunction = Container.Instance.Resolve<ISystemFunctionService>().GetEntity(4)
            });

            //id=9
            Container.Instance.Resolve<ISystemFunctionService>().Add(new SystemFunction()
            {
                SystemFunctionName = "查看投票结果",
                ClassName = "CQIE.OVS.Web.Controllers.PKInfoController",
                ControllerName = "VoteResult",
                ActionName = "Index",
                SortCode = 25,
                IsShow = true,
                ParentSysFunction = Container.Instance.Resolve<ISystemFunctionService>().GetEntity(4)
            });
        }

        public void InitRole()
        {
            //id=1
            Container.Instance.Resolve<ISysRoleService>().Add(new SysRole()
            {
                Name = "系统管理员",
                IsActive = 1,
                FunList = Container.Instance.Resolve<ISystemFunctionService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 1, 2, 3}) })
            });
            //id=2
            Container.Instance.Resolve<ISysRoleService>().Add(new SysRole()
            {
                Name = "业务管理员",
                IsActive = 1,
                FunList = Container.Instance.Resolve<ISystemFunctionService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 4, 5, 6, 7, 8, 9 }) })
            });
            //id=3
            Container.Instance.Resolve<ISysRoleService>().Add(new SysRole()
            {
                Name = "观众",
                IsActive = 1,
                FunList = Container.Instance.Resolve<ISystemFunctionService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 4, 8, 9 }) })
            });
        }

        public void InitUser()
        {
            Container.Instance.Resolve<ISysUserService>().Add(new SysUser()
            {
                Name = "系统管理员",
                IsActive = 1,
                Account = "Admin",
                Password = "123",
                RoleList = Container.Instance.Resolve<ISysRoleService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 1, 2, 3 }) })
            });
        }


    }
}