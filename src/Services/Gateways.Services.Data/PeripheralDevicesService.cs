namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Data.Common.Repositories;
    using Gateways.Data.Models;
    using Gateways.Services.Mapping;

    public class PeripheralDevicesService : IPeripheralDevicesService
    {
        private readonly IDeletableEntityRepository<PeripheralDevice> repository;

        public PeripheralDevicesService(IDeletableEntityRepository<PeripheralDevice> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.repository.All().To<T>().ToList();
        }

        public Task<PeripheralDevice> GetAsync(Guid id)
        {
            return this.repository.GetByIdWithDeletedAsync(id);
        }

        public async Task Create(PeripheralDevice gateway)
        {
            await this.repository.AddAsync(gateway);
            await this.repository.SaveChangesAsync();
        }

        public async Task Update(PeripheralDevice gateway)
        {
            this.repository.Update(gateway);
            await this.repository.SaveChangesAsync();
        }

        public async Task Delete(PeripheralDevice gateway)
        {
            this.repository.Delete(gateway);
            await this.repository.SaveChangesAsync();
        }
    }
}
