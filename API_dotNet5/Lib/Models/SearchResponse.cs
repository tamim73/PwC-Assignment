using System.Collections.Generic;

namespace Lib.Models
{
    public abstract class SearchResponse<T> : BaseResponse
    {
        public List<T> Rows { get; set; }
        public int Count { get; set; }
    }
}
