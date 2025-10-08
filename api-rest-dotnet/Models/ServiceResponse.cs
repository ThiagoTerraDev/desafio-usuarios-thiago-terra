namespace api_rest_dotnet.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
    }
}
