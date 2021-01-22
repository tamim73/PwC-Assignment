using Lib.Models;
using System.Linq;
using System.Linq.Dynamic.Core;


namespace Lib.Helpers
{
    public class SearchHelper
    {
        public static IQueryable<T> QuerySearchBatch<T>(IQueryable<T> queryablle, PageBatch searchBatch)
        {
            if (searchBatch.PageNo == 0)
                searchBatch.PageNo = 1;

            if (searchBatch.PageSize == 0)
                searchBatch.PageSize = 10;

            if (searchBatch.SortProperty == null)
                searchBatch.SortProperty = "Id";

            // sorting
            queryablle = queryablle.OrderBy(searchBatch.SortProperty + " " + searchBatch.SortOrder.ToString());

            // pagination
            queryablle = queryablle.Skip((searchBatch.PageNo - 1) * searchBatch.PageSize)
                                    .Take(searchBatch.PageSize);

            return queryablle;
        }
    }

}
