namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Data.Common.Repositories;
    using Gateways.Data.Models;
    using Gateways.Services.Mapping;

    public class GatewaysService : IGatewaysService
    {
        private readonly IDeletableEntityRepository<Gateway> gatewaysRepository;

        public GatewaysService(IDeletableEntityRepository<Gateway> gatewaysRepository)
        {
            this.gatewaysRepository = gatewaysRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.gatewaysRepository.All().To<T>().ToList();
        }

        public Task<Gateway> GetAsync(Guid id)
        {
            return this.gatewaysRepository.GetByIdWithDeletedAsync(id);
        }

        public async Task Create(Gateway gateway)
        {
            await this.gatewaysRepository.AddAsync(gateway);
            await this.gatewaysRepository.SaveChangesAsync();
        }

        public async Task Update(Gateway gateway)
        {
            this.gatewaysRepository.Update(gateway);
            await this.gatewaysRepository.SaveChangesAsync();
        }

        public async Task Delete(Gateway gateway)
        {
            this.gatewaysRepository.Delete(gateway);
            await this.gatewaysRepository.SaveChangesAsync();
        }

        public async Task AddDeviceAsync(Gateway gateway, PeripheralDevice device)
        {
            gateway.PeripheralDevices.Add(device);

            await this.Update(gateway);
        }

        public async Task RemoveDeviceAsync(Gateway gateway, PeripheralDevice device)
        {
            gateway.PeripheralDevices.Remove(device);

            await this.Update(gateway);
        }
    }
}
