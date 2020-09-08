using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CQIE.OVS.Domain;
using CQIE.OVS.Web.Apps;

namespace CQIE.OVS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult main()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Top()
        {
            ViewBag.LoginUser = AppHelper.LoginedUser.Name;
            return View();
        }

        public ActionResult MenuIndex()
        {
            ViewBag.PrivilegeList = AppHelper.Privileges;
            return View();
        }
    }
}