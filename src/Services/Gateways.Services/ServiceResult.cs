namespace Gateways.Services
{
    public class ServiceResult
    {
        public bool HasError => !string.IsNullOrEmpty(this.ErrorMessage);

        public string ErrorMessage { get; set; }
    }
}
