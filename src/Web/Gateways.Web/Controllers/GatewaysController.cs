﻿namespace Gateways.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Gateways.Data.Models;
    using Gateways.Services.Data;
    using Gateways.Services.Mapping;
    using Gateways.Web.ViewModels.Devices;

    using Microsoft.AspNetCore.Mvc;

    public class GatewaysController : BaseApiController
    {
        private readonly IGatewaysService gatewaysService;
        private readonly IPeripheralDevicesService peripheralDevicesService;

        public GatewaysController(IGatewaysService gatewaysService, IPeripheralDevicesService peripheralDevicesService)
        {
            this.gatewaysService = gatewaysService;
            this.peripheralDevicesService = peripheralDevicesService;
        }

        [HttpGet]
        public IActionResult GetGateways()
        {
            var gateways = this.gatewaysService.GetAll<GatewayViewModel>();

            return this.Json(gateways);
        }

        [HttpGet("{id}")]
        public IActionResult GetGateway(Guid id)
        {
            var gateway = this.gatewaysService.GetAsNoTracking(id);

            if (gateway == null)
            {
                return this.NotFound(new { id = id.ToString() });
            }

            var viewModel = AutoMapperConfig.MapperInstance.Map<Gateway, GatewayViewModel>(gateway);

            return this.Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GatewayInputModel model)
        {
            var gateway = AutoMapperConfig.MapperInstance.Map<GatewayInputModel, Gateway>(model);

            var result = await this.gatewaysService.CreateAsync(gateway);

            if (result.HasError)
            {
                return this.BadRequest(new { Error = result.ErrorMessage });
            }

            var viewModel = AutoMapperConfig.MapperInstance.Map<Gateway, GatewayViewModel>(gateway);

            return this.CreatedAtAction(nameof(this.Create), new { id = gateway.Id }, viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]GatewayEditModel model)
        {
            if (id != model.Id)
            {
                return this.BadRequest();
            }

            var gateway = await this.gatewaysService.GetAsync(id);
            if (gateway == null)
            {
                return this.NotFound();
            }

            AutoMapperConfig.MapperInstance.Map<GatewayEditModel, Gateway>(model, gateway);

            var result = await this.gatewaysService.UpdateAsync(gateway);

            if (result.HasError)
            {
                return this.BadRequest(new { Error = result.ErrorMessage });
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var gateway = await this.gatewaysService.GetAsync(id);
            if (gateway == null)
            {
                return this.NotFound();
            }

            var result = await this.gatewaysService.DeleteAsync(gateway);
            if (result.HasError)
            {
                return this.BadRequest(new { Error = result.ErrorMessage });
            }

            return this.NoContent();
        }

        [HttpPost("{id}/devices/{deviceId}")]
        public async Task<IActionResult> Add(Guid id, Guid deviceId)
        {
            var gateway = await this.gatewaysService.GetAsync(id);
            if (gateway == null)
            {
                return this.NotFound(new { Error = "Gateway not found" });
            }

            var device = await this.peripheralDevicesService.GetAsync(deviceId);
            if (device == null)
            {
                return this.NotFound(new { Error = "Peripheral device not found" });
            }

            var result = await this.gatewaysService.AddDeviceAsync(gateway, device);
            if (result.HasError)
            {
                return this.BadRequest(new { Error = result.ErrorMessage });
            }

            return this.NoContent();
        }

        [HttpDelete("{id}/devices/{deviceId}")]
        public async Task<IActionResult> Delete(Guid id, Guid deviceId)
        {
            var gateway = await this.gatewaysService.GetAsync(id);
            if (gateway == null)
            {
                return this.NotFound(new { id });
            }

            var device = await this.peripheralDevicesService.GetAsync(deviceId);
            if (device == null)
            {
                return this.NotFound(new { deviceId });
            }

            await this.gatewaysService.RemoveDeviceAsync(gateway, device);

            return this.NoContent();
        }
    }
}
