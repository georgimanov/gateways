namespace Gateways.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gateways.Data;
    using Gateways.Data.Common.Repositories;
    using Gateways.Data.Models;
    using Gateways.Data.Repositories;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class GatewaysServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Gateway>>();
            repository.Setup(r => r.AllAsNoTracking()).Returns(new List<Gateway>
                                                        {
                                                            new Gateway(),
                                                            new Gateway(),
                                                            new Gateway(),
                                                        }.AsQueryable());
            var service = new GatewaysService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GatewaysTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Gateways.Add(new Gateway());
            dbContext.Gateways.Add(new Gateway());
            dbContext.Gateways.Add(new Gateway());
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Gateway>(dbContext);
            var service = new GatewaysService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
