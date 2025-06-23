namespace Ecom.Api.helper
{
    public class ExceptionApi : BaseResponseApi
    {

        //private readonly string _details;

        public ExceptionApi(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {

            // _details = details;
            Details = details;
        }

        public string Details { get; set; }
    }
}
