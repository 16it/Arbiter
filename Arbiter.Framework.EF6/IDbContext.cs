namespace Arbiter.Framework.EF6
{
    using Arbiter.Framework.Contract;
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// 数据访问上下文接口
    /// </summary>
    public interface IDbContext<TPrimaryKey>
    {
        #region Properties

        /// <summary>
        /// 是否全局延迟加载
        /// </summary>
        bool LazyLoadingEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// 是否自动的跟踪数据的变化
        /// </summary>
        bool AutoDetectChangesEnabled
        {
            get;
            set;
        }

        /// <summary>
        ///是否创建动态生成的代理类的实例
        /// </summary>
        bool ProxyCreationEnabled
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 适合执行创建、更新、删除操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="doNotEnsureTransaction">如果存在事物，则使用；否则启用新事物操作</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// 将在此上下文中所做的所有更改保存到基础数据库。
        /// </summary>
        /// <returns>
        /// 影响行数
        /// </returns>
        int SaveChanges();

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name="validateOnSaveEnabled">保存时验证实体有效性，涉及到按需更新</param>
        /// <returns>影响行数</returns>
        int SaveChanges(bool validateOnSaveEnabled);

        /// <summary>
        /// 实体的集合
        /// </summary>
        /// <returns>集合</returns>
        IDbSet<T> Set<T>()
        where T : ModelBase<TPrimaryKey>;

        /// <summary>
        /// Sql语句执行，并将结果保存在数据实体中，适合执行查询操作
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>集合</returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        #endregion Methods
    }
}