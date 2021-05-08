namespace SGRE.TSA.Models
{
    public class ExternalServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T ResponseData { get; set; }
        public string  ErrorMessage { get; set; }
    }
}
