namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Data.Common.Repositories;
    using Gateways.Data.Models;
    using Gateways.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

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

        public Gateway GetAsNoTracking(Guid id)
        {
            return this.gatewaysRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<ServiceResult> Create(Gateway gateway)
        {
            var entry = this.gatewaysRepository.AllAsNoTrackingWithDeleted().FirstOrDefault(x => x.SerialNumber == gateway.SerialNumber);
            if (entry != null)
            {
                return new ServiceResult { ErrorMessage = $"Entry with SN {gateway.SerialNumber} already exists." };
            }

            await this.gatewaysRepository.AddAsync(gateway);
            await this.gatewaysRepository.SaveChangesAsync();

            return new ServiceResult();
        }

        public async Task<ServiceResult> Update(Gateway gateway)
        {
            var entry = this.gatewaysRepository.AllAsNoTrackingWithDeleted().FirstOrDefault(x => x.SerialNumber == gateway.SerialNumber);
            if (entry != null)
            {
                return new ServiceResult { ErrorMessage = $"Entry with SN {gateway.SerialNumber} already exists." };
            }

            this.gatewaysRepository.Update(gateway);
            await this.gatewaysRepository.SaveChangesAsync();

            return new ServiceResult();
        }

        public async Task Delete(Gateway gateway)
        {
            this.gatewaysRepository.HardDelete(gateway);
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

        public int GetCount()
        {
            return this.gatewaysRepository.AllAsNoTracking().Count();
        }
    }
}
