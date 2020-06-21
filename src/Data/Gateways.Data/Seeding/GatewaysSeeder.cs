namespace Gateways.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Data.Models;

    internal class GatewaysSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Gateways.Any())
            {
                return;
            }

            for (int i = 1; i < 5; i++)
            {
                await dbContext.Gateways.AddAsync(new Gateway { Name = $"Gateway {i}", IPv4 = $"127.0.0.{i}", SerialNumber = $"SN{i}" });
            }
        }
    }
}
