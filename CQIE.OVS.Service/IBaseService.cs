using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using CQIE.OVS.Domain;

namespace CQIE.OVS.Service
{
    /// <summary>
    /// 接口服务基类
    /// </summary>
    public interface IBaseService<T> where T : BaseEntity<T>, new()
    {
        #region 增加
        /// <summary>
        /// 新增实体；
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        #endregion

        #region 删除
        /// <summary>
        /// 根据ID删除
        /// </summary>
        void Delete(int id);
        /// <summary>
        /// 根据实体删除
        /// </summary>
        void Delete(T entity);
        /// <summary>
        /// 删除所有实体
        /// </summary>
        void DeleteAll();

        void DelByWhere(string strWhere);
        #endregion

        #region 修改
        /// <summary>
        /// 根据实体修改；
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        #endregion

        #region 查询
        /// <summary>
        /// 根据ID返回实体；
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetEntity(int id);

        IList<T> GetAll();

        /// <summary>
        /// 根据条件查询实体集合
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <returns></returns>
        IList<T> Query(IList<ICriterion> queryConditions);

        /// <summary>
        /// 分页查询功能；
        /// </summary>
        /// <param name="queryConditions">查询条件</param>
        /// <param name="orderList">排序列表</param>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="count">总记录数</param>
        /// <returns></returns>
        IList<T> Query(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count);
        #endregion

        IList<T> GetListBySQL(string strSql);
    }
}
