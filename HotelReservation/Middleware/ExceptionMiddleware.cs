using Ecom.Api.helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace HotelReservation.Api.Middleware
{
    public class ExceptionMiddleware
    {
        public class ExceptionsMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionsMiddleware> _logger;
            private readonly IHostEnvironment _env;
            private readonly IMemoryCache _memoryCache;
            private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
            public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger, IHostEnvironment env, IMemoryCache memoryCache)
            {
                _next = next;
                _logger = logger;
                _env = env;
                _memoryCache = memoryCache;
            }
            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    ApplySecurity(context);
                    if (IsRequestAllowed(context) == false)
                    {


                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

                        var response = new ExceptionApi(
                            context.Response.StatusCode,
                            message: "Too Many Request. Please try again later"
                        );

                        await context.Response.WriteAsJsonAsync(response);
                        // Accepts an object directly.

                        //Automatically serializes the object to JSON.

                        //Also sets the response header: Content - Type: applicat
                        //ion / json.
                    }
                    await _next(context); // Run the next middleware in the pipeline
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception occurred");

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var response = new ExceptionApi(
                        context.Response.StatusCode,
                        message: ex.Message,
                        details: _env.IsDevelopment() ? ex.StackTrace : null
                    );

                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var json = JsonSerializer.Serialize(response, options);

                    await context.Response.WriteAsync(json);
                }
            }
            private bool IsRequestAllowed(HttpContext context)
            {
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                //var ip = context.Connection.RemoteIpAddress.ToString();
                var cachKey = $"Rate:{ip}";
                var dateNow = DateTime.Now;
                // Best Practices for Cache Keys:
                //Use clear prefixes(e.g. "Rate:", "Token:", "Session:").

                //Keep it unique(IP, user ID, or route - based).

                //Avoid spaces or special characters.
                // timesStamp متى اخر مره عمل عندي ريكوست
                // count عدد ريكوستات خلال 30ثانيه
                var (timesStamp, count) = _memoryCache.GetOrCreate(cachKey, entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                    return (timesStamp: dateNow, count: 0);
                });

                if (dateNow - timesStamp < _rateLimitWindow)
                {
                    if (count >= 8)
                    {
                        return false;
                    }
                    _memoryCache.Set(cachKey, (timesStamp, count += 1), _rateLimitWindow);
                }
                else
                {
                    _memoryCache.Set(cachKey, (timesStamp, count), _rateLimitWindow);
                }
                return true;

            }
            private void ApplySecurity(HttpContext context)
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff"; //multi purpose internet mail exhange
                                                                                // ده بيمنع المتصفح إنه "يتخم" أو يحاول يتعرف على نوع المحتوى لوحده.
                                                                                //يعني لو الملف المفروض HTML، المتصفح مش هيحاول يتعامل معاه كـ JavaScript مثلًا.
                                                                                //ودي حماية ضد بعض أنواع الهجمات زي MIME sniffing.


                context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
                context.Response.Headers["X-Frame-Options"] = "DENY";
            }
        }
    }
}
