namespace Gateways.Web.ViewModels.Devices
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Gateways.Common;
    using Gateways.Data.Models;
    using Gateways.Services.Mapping;

    public class GatewayEditModel : GatewayInputModel
    {
        public Guid Id { get; set; }
    }
}
