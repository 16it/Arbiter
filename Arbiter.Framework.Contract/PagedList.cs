using System;
using System.Collections.Generic;

namespace Arbiter.Framework.Contract
{
    public class PagedList<T> : List<T>, IPagedList
    {
        #region Fields

        /// <summary>
        /// 记录结束索引
        /// </summary>
        public int EndRecordIndex => TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount;

        /// <summary>
        /// 数据开始索引
        /// </summary>
        public int StartRecordIndex => (CurrentPageIndex - 1) * PageSize + 1;

        /// <summary>
        /// 页总数
        /// </summary>
        public int TotalPageCount => (int)Math.Ceiling(TotalItemCount / (double)PageSize);

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">集合</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页总数</param>
        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            TotalItemCount = items.Count;
            CurrentPageIndex = pageIndex;

            for (int i = StartRecordIndex - 1; i < EndRecordIndex; i++)
            {
                Add(items[i]);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">集合</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页总数</param>
        /// <param name="totalItemCount">记录条数</param>
        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(items);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int CurrentPageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the extra count.
        /// </summary>
        public int ExtraCount
        {
            get;
            set;
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalItemCount
        {
            get;
            set;
        }

        #endregion Properties
    }
}