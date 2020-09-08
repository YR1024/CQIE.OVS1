using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.ActiveRecord;

namespace CQIE.OVS.Domain
{
    public class BaseEntity<T>:ActiveRecordBase
        where T : class
    {
        /// <summary>
        /// ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Native)]
        public virtual int ID { get; set; }
    }
}
