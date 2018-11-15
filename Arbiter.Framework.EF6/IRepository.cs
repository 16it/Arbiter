using Arbiter.Framework.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Arbiter.Framework.EF6
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<T, TPrimaryKey> where T : ModelBase<TPrimaryKey>
    {
        bool Delete(T entity);

        bool Delete(IEnumerable<T> entities);

        T GetFirstOrDefault(Expression<Func<T, bool>> conditions = null, params Expression<Func<T, object>>[] includes);

        T Get(object id);

        IQueryable<T> Query(Expression<Func<T, bool>> conditions = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        List<T> Get(Expression<Func<T, bool>> conditions = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includes);

        PagedList<T> FindAllByPage<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex);

        bool Insert(T entity);

        bool Insert(IEnumerable<T> entities);

        bool Update(T entity);

        bool Exist(Expression<Func<T, bool>> conditions = null);
    }
}