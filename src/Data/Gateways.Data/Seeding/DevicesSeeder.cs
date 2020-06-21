namespace Gateways.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Data.Models;

    internal class DevicesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PeripheralDevices.Any())
            {
                return;
            }

            for (int i = 1; i < 5; i++)
            {
                var peripheralDevices = new List<PeripheralDevice>();
                for (int j = 0; j < 10; j++)
                {
                    var status = j % 2 == 0 ? DeviseStatus.Online : DeviseStatus.Offline;
                    var device = new PeripheralDevice { DateOfCreation = DateTime.UtcNow, Status = status, UID = j, Vendor = $"Vendor {j}" };
                    peripheralDevices.Add(device);
                }

                var gateWay = new Gateway { Name = $"Gateway {i}", IPv4 = $"127.0.0.{i}", SerialNumber = $"SN{i}", PeripheralDevices = peripheralDevices };

                await dbContext.Gateways.AddAsync(gateWay);
            }
        }
    }
}
