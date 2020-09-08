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
    public class VoteController : Controller
    {
        // GET: Vote
        public ActionResult Index(int? pkinfoId)
        {
            int PkinfoId = pkinfoId ?? 0;
            PKInfo pkinfo = null;
            if(PkinfoId == 0)
            {
                IList<PKInfo> list = Container.Instance.Resolve<IPKInfoService>().Query(new List<ICriterion>() { Expression.Eq("IsActive", 1) });
                if (list != null)
                {
                    pkinfo = list[0];
                }
            }
            else
            {
                pkinfo = Container.Instance.Resolve<IPKInfoService>().GetEntity(PkinfoId);
            }
            
            ViewBag.PKInfo = pkinfo;
            ViewBag.PlayerOne = pkinfo.PlayerOne;
            ViewBag.PlayerTwo = pkinfo.PlayerTwo;
            return View();
        }

        //修改票数
        [HttpPost]
        public string Index(int pkinfoId, string player)
        {
            if (Session["user"]!=null)
            {
                PKInfo pkinfo = Container.Instance.Resolve<IPKInfoService>().GetEntity(pkinfoId);
                if (player == "playerone")
                {
                    pkinfo.PlayerOneTicket += 1;
                }
                else
                {
                    pkinfo.PlayerTwoTicket += 1;
                }
                Container.Instance.Resolve<IPKInfoService>().Update(pkinfo);
                return "true";
            }
            else
            {
                return "false";
            }
           
        }

        public ActionResult ShowTicket()
        {
            PKInfo pkinfo = null;
            IList<PKInfo> list = Container.Instance.Resolve<IPKInfoService>().Query(new List<ICriterion>() { Expression.Eq("IsActive", 1) });
            if (list != null)
            {
                pkinfo = list[0];
            }
            ViewBag.PKInfo = pkinfo;
            ViewBag.PlayerOne = pkinfo.PlayerOne;
            ViewBag.PlayerTwo = pkinfo.PlayerTwo;
            return View();
 
        }
        public JsonResult GetTicket(int? pkinfoId)
        {
            int PkinfoId = pkinfoId ?? 0;
            PKInfo pkinfo = null;
            if (PkinfoId == 0)
            {
                IList<PKInfo> list = Container.Instance.Resolve<IPKInfoService>().Query(new List<ICriterion>() { Expression.Eq("IsActive", 1) });
                if (list != null)
                {
                    pkinfo = list[0];
                }
            }
            else
            {
                pkinfo = Container.Instance.Resolve<IPKInfoService>().GetEntity(PkinfoId);
            }

            return Json(new
            {
                FirstPlayersScore = pkinfo.PlayerOneTicket,
                SecondPlayersScore = pkinfo.PlayerTwoTicket,
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public string Login(string account,string password)
        {
            string Password = AppHelper.EncodeMd5(password);
            IList<SysUser> userlist=Container.Instance.Resolve<ISysUserService>().GetAll();
            SysUser loginuser = null;
            foreach (var user in userlist)
            {
                if(user.Account==account && user.Password== Password)
                {
                    loginuser = user;
                    Session["user"] = loginuser;
                    return user.Name;
                }
            }           
            return "false";
        }
        [HttpPost]
        public string Logout()
        {
            Session["user"] = null;
            return "true";
        }
    }
}