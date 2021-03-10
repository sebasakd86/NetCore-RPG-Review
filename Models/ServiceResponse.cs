namespace Net_RPG.Models
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(){}
        public ServiceResponse(T data)
        {
            this.Data = data;
        }
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}