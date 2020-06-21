namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gateways.Data.Models;

    public interface IPeripheralDevicesService
    {
        IEnumerable<T> GetAll<T>();

        public Task<PeripheralDevice> GetAsync(Guid id);

        Task Create(PeripheralDevice gateway);

        Task Update(PeripheralDevice gateway);

        Task Delete(PeripheralDevice gateway);
    }
}
