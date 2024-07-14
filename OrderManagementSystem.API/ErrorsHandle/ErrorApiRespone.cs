namespace OrderManagementSystem.API.ErrorsHandle
{
    public class ErrorApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ErrorApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultErrorResponseMessage(StatusCode);
        }

        private string? GetDefaultErrorResponseMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You are Not Authorize",
                403 => "Forbidding",
                404 => "Not Found",
                500 => "server error",
                _ => null,

            };

        }
    }
}
