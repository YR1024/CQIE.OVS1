using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using CQIE.OVS.Domain;
using CQIE.OVS.Manager;
using CQIE.OVS.Service;

namespace CQIE.OVS.Component
{
    public class BaseComponent<T,M> : IBaseService<T>
        where T : BaseEntity<T>, new()
        where M : BaseManager<T>, new()
    {
        protected M manager = typeof(M).GetConstructor(Type.EmptyTypes).Invoke(null) as M;//Invoke反射

        #region 增
        /// <summary>
        /// 新建实体
        /// </summary>
        public virtual void Add(T entity)
        {
            manager.Add(entity);
        }
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        public void Delete(T t)
        {
            manager.Delete(t);
        }

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        public void Delete(int ID)
        {
            manager.Delete(ID);
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        public void DeleteAll()
        {
            manager.DelAll();
        }

        public void DelByWhere(string strWhere)
        {
            manager.DelByWhere(strWhere);
        }
        #endregion

        #region 改
        /// <summary>
        /// 更新实体
        /// </summary>
        public void Update(T t)
        {
            manager.Update(t);
        }
        #endregion

        #region 查
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        public T GetEntity(int ID)
        {
            return manager.Get(ID);
        }
        /// <summary>
        /// 获取所有实体
        /// </summary>
        public IList<T> GetAll()
        {
            return manager.GetAll();
        }
        /// <summary>
        /// 根据查询条件查询满足条件的实体
        /// </summary>
        public IList<T> Query(IList<ICriterion> queryConditions)
        {
            return manager.Query(queryConditions);
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
            return manager.Query(queryConditions, orderList, pageIndex, pageSize, out count);
        }
        #endregion

        public IList<T> GetListBySQL(string strSql)
        {
            return manager.GetListBySQL(strSql);
        }
        //public IList<T> Query(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
