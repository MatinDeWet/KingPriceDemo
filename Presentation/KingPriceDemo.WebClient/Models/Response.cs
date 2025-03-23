namespace KingPriceDemo.WebClient.Models
{
    public class Response<T>
    {
        public string Message { get; set; } = null!;

        public string ValidationErrors { get; set; } = null!;

        public bool Success { get; set; } = true;

        public T Data { get; set; } = default!;
    }
}
