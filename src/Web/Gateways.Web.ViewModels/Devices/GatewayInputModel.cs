namespace Gateways.Web.ViewModels.Devices
{
    using System.ComponentModel.DataAnnotations;

    using Gateways.Common;
    using Gateways.Data.Models;
    using Gateways.Services.Mapping;

    public class GatewayInputModel : IMapTo<Gateway>
    {
        public string SerialNumber { get; set; }

        public string Name { get; set; }

        [RegularExpression(GlobalConstants.IPv4RexEx)]
        public string IPv4 { get; set; }
    }
}
