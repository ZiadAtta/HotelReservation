using Ecom.Api.helper;

namespace HotelReservation.Api.Helper
{
    public class ResponseApi<T> : BaseResponseApi
    {
        public ResponseApi(int statusCode, string? message = null, T? data = default)
        : base(statusCode, message)
        {
            Data = data;
        }

        public T? Data { get; set; }
    }
}
