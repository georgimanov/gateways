namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Common;
    using Gateways.Data.Common.Repositories;
    using Gateways.Data.Models;
    using Gateways.Services.Mapping;

    public class GatewaysService : IGatewaysService
    {
        private readonly IDeletableEntityRepository<Gateway> gatewaysRepository;
        private readonly IDeletableEntityRepository<PeripheralDevice> peripheralDevicesRepository;

        public GatewaysService(IDeletableEntityRepository<Gateway> gatewaysRepository, IDeletableEntityRepository<PeripheralDevice> peripheralDevicesRepository)
        {
            this.gatewaysRepository = gatewaysRepository;
            this.peripheralDevicesRepository = peripheralDevicesRepository;
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

        public async Task<ServiceResult> CreateAsync(Gateway gateway)
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

        public async Task<ServiceResult> UpdateAsync(Gateway gateway)
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

        public async Task<ServiceResult> DeleteAsync(Gateway gateway)
        {
            var hasDevices = this.peripheralDevicesRepository.AllAsNoTrackingWithDeleted().Any(x => x.GatewayId == gateway.Id);
            if (hasDevices)
            {
                return new ServiceResult { ErrorMessage = $"Gateway has assigned peripheral devices." };
            }

            this.gatewaysRepository.HardDelete(gateway);
            await this.gatewaysRepository.SaveChangesAsync();

            return new ServiceResult();
        }

        public async Task<ServiceResult> AddDeviceAsync(Gateway gateway, PeripheralDevice device)
        {
            var count = this.peripheralDevicesRepository.AllAsNoTrackingWithDeleted().Count(x => x.GatewayId == gateway.Id);

            if (count < GlobalConstants.GatewayMaxPeripheralDevicesCount)
            {
                gateway.PeripheralDevices.Add(device);

                device.GatewayId = gateway.Id;
                this.peripheralDevicesRepository.Update(device);
                await this.peripheralDevicesRepository.SaveChangesAsync();

                return new ServiceResult();
            }

            return new ServiceResult() { ErrorMessage = $"Gateway max peripheral devices count of {GlobalConstants.GatewayMaxPeripheralDevicesCount} is reached." };
        }

        public async Task RemoveDeviceAsync(Gateway gateway, PeripheralDevice device)
        {
            gateway.PeripheralDevices.Remove(device);

            device.GatewayId = null;
            this.peripheralDevicesRepository.Update(device);
            await this.peripheralDevicesRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.gatewaysRepository.AllAsNoTracking().Count();
        }
    }
}
