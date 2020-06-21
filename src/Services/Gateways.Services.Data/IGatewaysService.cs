namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gateways.Data.Models;

    public interface IGatewaysService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        public Task<Gateway> GetAsync(Guid id);

        public Gateway GetAsNoTracking(Guid id);

        Task<ServiceResult> CreateAsync(Gateway gateway);

        Task<ServiceResult> UpdateAsync(Gateway gateway);

        Task<ServiceResult> DeleteAsync(Gateway gateway);

        Task<ServiceResult> AddDeviceAsync(Gateway gateway, PeripheralDevice device);

        Task RemoveDeviceAsync(Gateway gateway, PeripheralDevice device);
    }
}
