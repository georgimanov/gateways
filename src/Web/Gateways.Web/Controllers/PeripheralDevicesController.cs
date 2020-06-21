namespace Gateways.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Gateways.Data.Models;
    using Gateways.Services.Data;
    using Gateways.Services.Mapping;
    using Gateways.Web.ViewModels.Devices;
    using Microsoft.AspNetCore.Mvc;

    public class PeripheralDevicesController : BaseApiController
    {
        private readonly IPeripheralDevicesService peripheralDevicesService;

        public PeripheralDevicesController(IPeripheralDevicesService peripheralDevicesService)
        {
            this.peripheralDevicesService = peripheralDevicesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeripheralDeviceInputModel model)
        {
            var device = AutoMapperConfig.MapperInstance.Map<PeripheralDeviceInputModel, PeripheralDevice>(model);

            await this.peripheralDevicesService.Create(device);

            var viewModel = AutoMapperConfig.MapperInstance.Map<PeripheralDevice, PeripheralDeviceViewModel>(device);

            return this.CreatedAtAction(nameof(this.Create), new { id = viewModel.Id }, viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var device = await this.peripheralDevicesService.GetAsync(id);
            if (device == null)
            {
                return this.NotFound();
            }

            await this.peripheralDevicesService.Delete(device);

            return this.NoContent();
        }
    }
}
