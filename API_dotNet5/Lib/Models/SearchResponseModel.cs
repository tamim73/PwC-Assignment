using System.Collections.Generic;

namespace Lib.Models
{
    public class SearchResponseModel<T> : ResponseModel
    {
        public SearchResponseModel(string message, bool hasError = false) : base(message, hasError)
        {

        }

        public List<T> Rows { get; set; }
        public int Count { get; set; }

    }
}
