using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Adbeniz.Weather.Restful.Application.Handlers.Cities;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Infrastructure.Data;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Adbeniz.Weather.Restful.Test.RespositoryTest
{
    public class CityRepositoryCommandTest
    {
		 private IEnumerable<City> GetTestData()
        {
            A.Configure<City>()
            .Fill(x => x.ID)
            .Fill(x => x.Name).AsCity()
            .Fill(x => x.Country,() => {

				string[] country = {"Argentina", "Uruguay","Bolivia", "Peru","Chile","Colombia"};
				Random random = new Random();
				int index = random.Next(country.Length);

				return country[index];})
            .Fill(x => x.Enable, true);

            var list = A.ListOf<City>(10);
            list[0].ID = 100;
            list[0].Name = "San Salvador de Jujuy";
			list[0].Country = "Argentina";
			list[0].Enable = true;

            return list;
        }

		private Mock<ClimasDbContext> CreateDbContext()
        {
            var testData = GetTestData().AsQueryable();
            var dbSet = new Mock<DbSet<City>>();

            dbSet.As<IQueryable<City>>().Setup(x => x.Provider).Returns(testData.Provider);
            dbSet.As<IQueryable<City>>().Setup(x => x.Expression).Returns(testData.Expression);
            dbSet.As<IQueryable<City>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            dbSet.As<IQueryable<City>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());
            dbSet.As<IAsyncEnumerable<City>>().Setup(x=>x.GetAsyncEnumerator(new CancellationToken()))
            .Returns(new AsyncEnumerator<City>(testData.GetEnumerator()));
            dbSet.As<IQueryable<City>>().Setup(x=>x.Provider).Returns(new AsyncQueryProvider<City>(testData.Provider));

            var dbContext = new Mock<ClimasDbContext>();
            dbContext.Setup(x => x.Cities).Returns(dbSet.Object);

            return dbContext;
        }

		private Mock<IRepositoryQuery<ClimasDbContext, City>> CreateRepositoryQuery()
        {
            var testData = GetTestData().AsQueryable();
            var dbSet = new Mock<DbSet<City>>();

            dbSet.As<IQueryable<City>>().Setup(x => x.Provider).Returns(testData.Provider);
            dbSet.As<IQueryable<City>>().Setup(x => x.Expression).Returns(testData.Expression);
            dbSet.As<IQueryable<City>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            dbSet.As<IQueryable<City>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

            dbSet.As<IAsyncEnumerable<City>>().Setup(x=>x.GetAsyncEnumerator(new CancellationToken()))
            .Returns(new AsyncEnumerator<City>(testData.GetEnumerator()));

            dbSet.As<IQueryable<City>>().Setup(x=>x.Provider).Returns(new AsyncQueryProvider<City>(testData.Provider));

            var mockRepository = new Mock<IRepositoryQuery<ClimasDbContext, City>>();
            mockRepository.Setup(x => x.GetAll()).Returns(dbSet.Object.ToList<City>());
            return mockRepository;
        }

		private Mock<IRepositoryQuery<ClimasDbContext, City>> CreateRepository(ClimasDbContext db)
        {
            var testData = GetTestData().AsQueryable();
            var dbSet = new Mock<DbSet<City>>();

            dbSet.As<IQueryable<City>>().Setup(x => x.Provider).Returns(testData.Provider);
            dbSet.As<IQueryable<City>>().Setup(x => x.Expression).Returns(testData.Expression);
            dbSet.As<IQueryable<City>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            dbSet.As<IQueryable<City>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

            dbSet.As<IAsyncEnumerable<City>>().Setup(x=>x.GetAsyncEnumerator(new CancellationToken()))
            .Returns(new AsyncEnumerator<City>(testData.GetEnumerator()));

            dbSet.As<IQueryable<City>>().Setup(x=>x.Provider).Returns(new AsyncQueryProvider<City>(testData.Provider));

            var mockRepository = new Mock<IRepositoryQuery<ClimasDbContext, City>>();
			mockRepository.Setup(x=>x.GetAll()).Returns(db.Cities);
            return mockRepository;
        }

		[Fact]
		public async void GetAllCities()
		{
			// Given
			string primerCiudad = "San Salvador de Jujuy";
			var mockLogger = new Mock<ILogger<CityGetAllHandler>>();
			var dbContextMock = new ClimasDbContext();
			dbContextMock.Cities = CreateDbContext().Object.Cities;

			var mockCityRepositoryQuery = new Repository<ClimasDbContext, City>(dbContextMock,false);

			var cityGetAllHandler = new Mock<CityGetAllHandler>(mockLogger.Object, mockCityRepositoryQuery);
			// When
			var cityList = await cityGetAllHandler.Object.Handle(new CityGetAllHandlerRequest(), new CancellationToken());
			// Then
			Assert.NotEmpty(cityList);
			Assert.NotNull(cityList.FirstOrDefault(x => x.City == primerCiudad));
		}

    }
}
