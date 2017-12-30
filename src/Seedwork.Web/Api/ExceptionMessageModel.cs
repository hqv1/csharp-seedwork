namespace Hqv.Seedwork.Web.Api
{
    /// <summary>
    /// The model we'll be returning on an exception
    /// </summary>
    public class ExceptionMessageModel
    {
        public ExceptionMessageModel()
        {

        }

        public ExceptionMessageModel(int code, string message, object detail = null)
        {
            Code = code;
            Message = message;
            Detail = detail;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public object Detail { get; set; }
    }
}