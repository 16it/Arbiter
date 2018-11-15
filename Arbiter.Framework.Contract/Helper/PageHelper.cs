using System.Collections.Generic;
using System.Linq;

namespace Arbiter.Framework.Contract.Helper
{
    public static class PageHelper
    {
        public static PagedList<T> ToPagedList<T>
          (
              this IQueryable<T> allItems,
              int pageIndex,
              int pageSize
          )
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int itemIndex = (pageIndex - 1) * pageSize;
            List<T> pageOfItems = allItems.Skip(itemIndex).Take(pageSize).ToList();
            int totalItemCount = allItems.Count();
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }
    }
}