namespace Gateways.Web.ViewModels.Devices
{
    using System;
    using System.Collections.Generic;

    using Gateways.Data.Models;
    using Gateways.Services.Mapping;

    public class GatewayViewModel : IMapFrom<Gateway>
    {
        private IEnumerable<PeripheralDeviceViewModel> peripheralDevices;

        public GatewayViewModel()
        {
            this.peripheralDevices = new List<PeripheralDeviceViewModel>();
        }

        public Guid Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IPv4 { get; set; }

        public IEnumerable<PeripheralDeviceViewModel> PeripheralDevices
        {
            get => this.peripheralDevices;

            set => this.peripheralDevices = value;
        }
    }
}
