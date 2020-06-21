﻿namespace Gateways.Web.ViewModels.Devices
{
    using System;

    using Gateways.Data.Models;
    using Gateways.Services.Mapping;

    public class PeripheralDeviceViewModel : IMapFrom<PeripheralDevice>
    {
        public Guid Id { get; set; }

        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DeviseStatus Status { get; set; }
    }
}
