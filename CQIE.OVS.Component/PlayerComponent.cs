using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQIE.OVS.Domain;
using CQIE.OVS.Manager;
using CQIE.OVS.Service;

namespace CQIE.OVS.Component
{
    public class PlayerComponent : BaseComponent<Player, PlayerManager>, IPlayerService
    {
    }
}
