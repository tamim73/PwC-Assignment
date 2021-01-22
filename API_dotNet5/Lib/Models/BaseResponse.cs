namespace Lib.Models
{
    public abstract class BaseResponse
    {
        public string Message { get; set; }
        public bool HasError { get; set; }

    }
}
