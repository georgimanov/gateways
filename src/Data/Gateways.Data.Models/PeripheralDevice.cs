namespace Gateways.Data.Models
{
    using System;

    using Gateways.Data.Common.Models;

    public class PeripheralDevice : BaseDeletableModel<Guid>
    {
        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DeviseStatus Status { get; set; }
    }
}
