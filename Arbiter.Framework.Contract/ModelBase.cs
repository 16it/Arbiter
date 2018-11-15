namespace Arbiter.Framework.Contract
{
    using System;

    /// <summary>
    /// 数据库实体基类
    /// </summary>
    public abstract class ModelBase<TPrimaryKey>
    {
        #region Constructors

        public ModelBase()
        {
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.MinValue;
            Available = true;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 是否可以用
        /// </summary>
        public virtual bool Available
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime ModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual TPrimaryKey ID
        {
            get;
            set;
        }

        #endregion Properties
    }
}