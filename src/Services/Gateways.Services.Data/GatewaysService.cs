namespace Gateways.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Data.Common.Repositories;
    using Gateways.Data.Models;
    using Gateways.Services.Mapping;
    using Gateways.Web.ViewModels.Devices;

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
    }
}
