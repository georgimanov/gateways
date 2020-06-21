namespace Gateways.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Gateways.Common;
    using Gateways.Data.Common.Models;

    public class Gateway : BaseDeletableModel<Guid>
    {
        private ICollection<PeripheralDevice> peripheralDevices;

        public Gateway()
        {
            this.peripheralDevices = new HashSet<PeripheralDevice>();
        }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        [RegularExpression(GlobalConstants.IPv4RexEx)]
        public string IPv4 { get; set; }

        public virtual ICollection<PeripheralDevice> PeripheralDevices
        {
            get => this.peripheralDevices;

            set => this.peripheralDevices = value;
        }
    }
}
