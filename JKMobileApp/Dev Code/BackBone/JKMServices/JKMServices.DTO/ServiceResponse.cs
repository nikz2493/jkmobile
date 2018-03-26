namespace JKMServices.DTO
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public string Information { get; set; }
        public string BadRequest { get; set; }
    }
}