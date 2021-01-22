using Lib.Enums;

namespace Lib.Models
{
    public class PageBatch
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
