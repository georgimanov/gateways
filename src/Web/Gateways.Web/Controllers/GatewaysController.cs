namespace Gateways.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Gateways.Services.Data;
    using Gateways.Web.ViewModels.Devices;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/Gateways")]
    public class GatewaysController : BaseController
    {
        private readonly IGatewaysService gatewaysService;

        public GatewaysController(IGatewaysService gatewaysService)
        {
            this.gatewaysService = gatewaysService;
        }

        [HttpGet]
        public IActionResult GetGateways()
        {
            var gateways = this.gatewaysService.GetAll<GatewayViewModel>();

            return this.Json(gateways);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGateway(Guid id)
        {
            var gateway = await this.gatewaysService.GetAsync(id);

            if (gateway == null)
            {
                return this.NotFound(new { id = id.ToString() });
            }

            return this.Json(gateway);
        }
    }
}
