using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OVS.Domain;
using NHibernate.Criterion;

namespace CQIE.OVS.Manager
{
    public class SysUserManager : BaseManager<SysUser>
    {
        public SysUser Login(string account ,string pwd)
        {
            SysUser user = new SysUser();
            IList<ICriterion> query = new List<ICriterion>();
            query.Add(Expression.Eq("Account",account));
            query.Add(Expression.Eq("Password",pwd));
            query.Add(Expression.Eq("IsActive",1));
            user = Query(query).ToList().FirstOrDefault();
            return user;
        }
    }
}
