namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gateways.Data.Models;

    public interface IGatewaysService
    {
        IEnumerable<T> GetAll<T>();

        public Task<Gateway> GetAsync(Guid id);

        Task Create(Gateway gateway);

        Task Update(Gateway gateway);

        Task Delete(Gateway gateway);

        Task AddDeviceAsync(Gateway gateway, PeripheralDevice device);

        Task RemoveDeviceAsync(Gateway gateway, PeripheralDevice device);
    }
}
