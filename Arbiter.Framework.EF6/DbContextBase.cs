namespace Arbiter.Framework.EF6
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    /// <summary>
    /// 基于EF数据上下文实现
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="Arbiter.Framework.EF6.IDbContext{TPrimaryKey}" />
    public abstract class DbContextBase<TPrimaryKey> : DbContext, IDbContext<TPrimaryKey>
    {
        #region Constructors

        public DbContextBase(DbConnection dbConnection)
        : base(dbConnection, true)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 是否自动的跟踪数据的变化
        /// </summary>
        public virtual bool AutoDetectChangesEnabled
        {
            get => Configuration.AutoDetectChangesEnabled;
            set => Configuration.AutoDetectChangesEnabled = value;
        }

        /// <summary>
        /// 是否全局延迟加载
        /// </summary>
        public bool LazyLoadingEnabled
        {
            get => Configuration.LazyLoadingEnabled;
            set => Configuration.LazyLoadingEnabled = value;
        }

        /// <summary>
        /// 是否创建动态生成的代理类的实例
        /// </summary>
        public virtual bool ProxyCreationEnabled
        {
            get => Configuration.ProxyCreationEnabled;
            set => Configuration.ProxyCreationEnabled = value;
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
        /// <returns>
        /// 影响行数
        /// </returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;

            if (timeout.HasValue)
            {
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            TransactionalBehavior transactionalBehavior = doNotEnsureTransaction
                    ? TransactionalBehavior.DoNotEnsureTransaction
                    : TransactionalBehavior.EnsureTransaction;
            int result = parameters != null ? Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters) : Database.ExecuteSqlCommand(transactionalBehavior, sql);

            if (timeout.HasValue)
            {
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return result;
        }

        /// <summary>
        /// 实体的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// 集合
        /// </returns>
        IDbSet<T> IDbContext<TPrimaryKey>.Set<T>()
        {
            return base.Set<T>();
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name="validateOnSaveEnabled">保存时验证实体有效性，涉及到按需更新</param>
        /// <returns>
        /// 影响行数
        /// </returns>
        public int SaveChanges(bool validateOnSaveEnabled)
        {
            bool onSaveEnabled = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;

            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                return SaveChanges();
            }
            finally
            {
                if (onSaveEnabled)
                {
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }

        /// <summary>
        /// Sql语句执行，并将结果保存在数据实体中，适合执行查询操作
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>
        /// 集合
        /// </returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters);
        }

        #endregion Methods
    }
}