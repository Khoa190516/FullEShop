using BussinesLayer.ResponseModels.ApiResponseModel;
using Newtonsoft.Json;

namespace EShopAPI.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                var response = new ApiResponse();
                await WriteResponseAsync(context, response.SetApiResponse(
                    statusCode: System.Net.HttpStatusCode.InternalServerError,
                    isSuccess: false,
                    message: ex.Message,
                    result: null));
            }
        }

        private static async Task WriteResponseAsync(HttpContext context, ApiResponse response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
