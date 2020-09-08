using CQIE.OVS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQIE.OVS.Web.Apps
{
    public class AppHelper
    {
        //使用MD5加密传入的字符
        public static string EncodeMd5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
        }

        //保存登录用户的信息
        public static SysUser LoginedUser
        {
            get
            {
                //如果Session信息不为空
                if (HttpContext.Current.Session["LoginedUser"] != null)
                {
                    //将Session登录信息转换为User并返回
                    return HttpContext.Current.Session["LoginedUser"] as SysUser;
                }
                return null;
            }
            set
            {
                //将传入的信息保存到Session中
                HttpContext.Current.Session["LoginedUser"] = value;
            }
        }

        //获取主机的地址
        public static string Host
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsoluteUri;
            }
        }

        public static string GetRootURI()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;

                string UrlAuthority = Req.Url.GetLeftPart(System.UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在 Web 站点 
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下 
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }

        //保存当前登录用户的菜单权限
        public static IList<SystemFunction> privileges;
        public static IList<SystemFunction> Privileges
        {
            get
            {
                //获取系统中所有的功能模块
                //privileges = Container.Instance.Resolve<ISystemFunctionService>().GetAll();
                //return privileges;


                if (privileges != null)
                {
                    return privileges;
                }
                IList<SystemFunction> privilegeList = null;//声明一个集合变量
                if (LoginedUser != null && LoginedUser.RoleList != null)//如果有用户登录并且有对应的角色
                {
                    foreach (SysRole role in LoginedUser.RoleList)//遍历当前用户的角色
                    {
                        if (role != null)
                        {
                            foreach (SystemFunction function in role.FunList)//获取当前用户可以操作的功能
                            {
                                if (privilegeList == null)
                                {
                                    privilegeList = new List<SystemFunction>();//实例化权限集合
                                }
                                #region 判断教师、领导评教菜单
                                //int[] arr = new int[] { 19, 20, 21 };
                                //if (arr.Contains(function.ID))
                                //{
                                //    Teacher entity = Container.Instance.Resolve<ITeacherService>().GetAll().Where(o => o.SysUser.ID == LoginedUser.ID).FirstOrDefault();
                                //    if (entity == null)
                                //    { continue; }
                                //    int position = entity.Position;
                                //    if (position == 1)
                                //    {
                                //        if (function.ID == 21)
                                //        {
                                //            if (privilegeList.Where(o => o.ID == function.ID).Count() < 1)//如果当前功能在权限集合中不存在
                                //            {
                                //                if (function.IsShow == true)
                                //                {
                                //                    privilegeList.Add(function);//添加到集合中
                                //                }
                                //            }
                                //        }
                                //    }
                                //    if (position == 2)
                                //    {
                                //        if (function.ID == 20)
                                //        {
                                //            if (privilegeList.Where(o => o.ID == function.ID).Count() < 1)//如果当前功能在权限集合中不存在
                                //            {
                                //                if (function.IsShow == true)
                                //                {
                                //                    privilegeList.Add(function);//添加到集合中
                                //                }
                                //            }
                                //        }
                                //    }
                                //    if (position == 3)
                                //    {
                                //        if (function.ID == 19)
                                //        {
                                //            if (privilegeList.Where(o => o.ID == function.ID).Count() < 1)//如果当前功能在权限集合中不存在
                                //            {
                                //                if (function.IsShow == true)
                                //                {
                                //                    privilegeList.Add(function);//添加到集合中
                                //                }
                                //            }
                                //        }
                                //    }
                                //}
                                #endregion
                                else
                                {
                                    if (function.ParentSysFunction != null)
                                    {
                                        if (privilegeList.Where(o => o.ID == function.ParentSysFunction.ID).Count() < 1)
                                        {
                                            privilegeList.Add(function.ParentSysFunction);
                                        }
                                    }


                                    if (privilegeList.Where(o => o.ID == function.ID).Count() < 1)//如果当前功能在权限集合中不存在
                                    {
                                        if (function.IsShow == true)
                                        {
                                            privilegeList.Add(function);//添加到集合中
                                        }
                                    }
                                }
                            }
                        }
                    }
                    privilegeList.OrderBy(o => o.SortCode);
                }
                return privilegeList;

            }

        }
    }
}