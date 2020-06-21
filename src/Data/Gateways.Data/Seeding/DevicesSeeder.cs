namespace Gateways.Data.Seeding
{
    using System;
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

            for (int i = 1; i <= 50; i++)
            {
                var device = new PeripheralDevice { UID = i, DateOfCreation = DateTime.UtcNow, Status = i % 2 == 0 ? DeviseStatus.Offline : DeviseStatus.Online, Vendor = "Seed Vendor Ltd." };
                await dbContext.PeripheralDevices.AddAsync(device);
            }
        }
    }
}
