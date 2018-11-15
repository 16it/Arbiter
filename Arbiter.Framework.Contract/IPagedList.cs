namespace Arbiter.Framework.Contract
{
    /// <summary>
    /// 分页接口
    /// </summary>
    public interface IPagedList
    {
        #region Properties

        /// <summary>
        /// 当前页数
        /// </summary>
        int CurrentPageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 总记录
        /// </summary>
        int TotalItemCount
        {
            get;
            set;
        }

        #endregion Properties
    }
}