using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OVS.Domain;

namespace CQIE.OVS.Service
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        SysUser Login(string account, string pwd);
    }
}
