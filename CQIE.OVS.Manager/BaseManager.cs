using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using NHibernate;

namespace CQIE.OVS.Manager
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public class BaseManager<T> : ActiveRecordBase<T>
        where T : class
    {
        #region 增
        /// <summary>
        /// 新建实体
        /// </summary>
        public virtual void Add(T t)
        {
            ActiveRecordBase.Create(t);
        }
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        public new void Delete(T t)
        {
            ActiveRecordBase.Delete(t);
        }

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        public void Delete(int id)
        {
            T t = Get(id);//根据id获取对象
            if (t != null)//如果对象存在
            {
                Delete(t);//删除它
            }
        }

        /// <summary>
        /// 删除全部
        /// </summary>
        public void DelAll()
        {
            ActiveRecordBase.DeleteAll(typeof(T));
        }

        public void DelByWhere(string strWhere)
        {
            ActiveRecordBase.DeleteAll(typeof(T), strWhere);
        }
        #endregion

        #region 改
        /// <summary>
        /// 更新实体
        /// </summary>
        public new void Update(T t)
        {
            ActiveRecordBase.Update(t);
        }
        #endregion

        #region 查
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        public T Get(int ID)
        {
            return FindByPrimaryKey(typeof(T), ID) as T;
        }
        /// <summary>
        /// 获取所有实体
        /// </summary>
        public IList<T> GetAll()
        {
            return FindAll(typeof(T)) as IList<T>;
        }
        /// <summary>
        /// 根据查询条件查询满足条件的实体
        /// </summary>
        public IList<T> Query(IList<ICriterion> queryConditions)
        {
            Array arr = ActiveRecordBase.FindAll(typeof(T), queryConditions.ToArray());
            return arr as IList<T>;
        }
        /// <summary>
        /// 根据SQL语句查询
        /// </summary>
        /// <param name="querySql"></param>
        /// <returns></returns>
        public IList<T> FindBySQL(string querySql)
        {
            ISession session = ActiveRecordBase.holder.CreateSession(typeof(T));//获取管理DeliveryForm的session对象
            //            querySql = string.Format(@"with cte as 
            // ( 
            // select a.ID,a.OrganizationName,a.ParentID from Organization a where id={0}
            // union all  
            // select k.ID,k.OrganizationName,k.ParentID  from Organization k inner join cte c on c.id = k.ParentID 
            // )select * from cte ", pId);
            IQuery query = session.CreateSQLQuery(querySql).AddEntity("arr", typeof(T));//获取满足条件的数据
            IList<T> arr = query.List<T>();
            session.Close();//关闭session
            return arr;
        }

        /// <summary>
        /// 分页获取满足条件的实体
        /// </summary>
        /// <param name="queryConditions">查询条件</param>
        /// <param name="orderList">排序属性列表</param>
        /// <param name="pageIndex">页码,从1开始</param>
        /// <param name="pageSize">每页实体数</param>
        /// <param name="count">满足条件的实体总数</param>
        /// <returns></returns>
        public IList<T> Query(IList<ICriterion> queryConditions, IList<Order> orderList,
            int pageIndex, int pageSize, out int count)
        {
            if (queryConditions == null)//如果为null则赋值为一个总数为0的集合
            {
                queryConditions = new List<ICriterion>();
            }
            if (orderList == null)//如果为null则赋值为一个总数为0的集合
            {
                orderList = new List<Order>();
            }
            count = Count(typeof(T), queryConditions.ToArray());//根据查询条件获取满足条件的对象总数
            Array arr = SlicedFindAll(typeof(T), (pageIndex - 1) * pageSize, pageSize, orderList.ToArray(),
                queryConditions.ToArray());//根据查询条件分页获取对象集合
            return arr as IList<T>;
        }
        #endregion

        /// <summary>
        /// 根据SQL语句获取数据集；
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public IList<T> GetListBySQL(string strSql)
        {
            return FindBySQL(strSql);
        }
    }
}
