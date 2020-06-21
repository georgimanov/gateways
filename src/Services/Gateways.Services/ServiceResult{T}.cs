namespace Gateways.Services
{
    public class ServiceResult<T>
        where T : class
    {
        public bool HasError => !string.IsNullOrEmpty(this.ErrorMessage);

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}
