using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CQIE.OVS.Domain;
using NHibernate;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace CQIE.OVS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (!ActiveRecordStarter.IsInitialized)
            {
                //如果ActiveRecordStarter框架没有初始化
                IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
                ActiveRecordStarter.Initialize(typeof(SysUser).Assembly, source);
            }
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
