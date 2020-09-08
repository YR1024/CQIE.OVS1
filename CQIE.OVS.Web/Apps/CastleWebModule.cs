using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace CQIE.OVS.Web.Apps
{
    public class CastleWebModule : IHttpModule
    {
        protected static readonly String sessionKey = "nhibernate.session";
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnBeginRequest);
            context.EndRequest += new EventHandler(OnEndRequest);
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items.Add(sessionKey, new SessionScope(FlushAction.Never));
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            SessionScope sessionScope = (SessionScope)HttpContext.Current.Items[sessionKey];

            if (sessionScope != null)
            {
                sessionScope.Flush();//事务提交

                if (sessionScope.WantsToCreateTheSession == false)
                {
                    sessionScope.Dispose();
                }
            }
        }
    }
}