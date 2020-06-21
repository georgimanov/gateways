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
    }
}
