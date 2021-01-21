namespace Lib.Models
{
    public class ResponseModel
    {
        public ResponseModel(string message, bool hasError = false)
        {
            Message = message;
            HasError = hasError;
        }
        public string Message { get; set; }
        public bool HasError { get; set; }

    }
}
