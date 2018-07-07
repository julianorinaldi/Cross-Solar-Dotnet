using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTests
    {

        private readonly AnalyticsController _analyticsController;

        private readonly Mock<IAnalyticsRepository> _analyticsRepositoryMock = new Mock<IAnalyticsRepository>();
        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();

        public AnalyticsControllerTests()
        {
            _analyticsController = new AnalyticsController(_analyticsRepositoryMock.Object, _panelRepositoryMock.Object);
        }

        [Fact]
        public async Task Register_GetAnalystics()
        {
            string panelId = "AAAA1111BBBB2222";

            var panel = new Panel
            {
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = panelId
            };

            var oneHourElectricity = new OneHourElectricity()
            {
                DateTime = new DateTime(2018, 7, 7),
                Id = 1,
                KiloWatt = 100,
                PanelId = panelId
            };

            
            _panelRepositoryMock.Setup(m => m.Query()).Returns(new List<Panel>() { panel }.AsQueryable());
            _analyticsRepositoryMock.Setup(m => m.Query()).Returns(new List<OneHourElectricity>() { oneHourElectricity }.AsQueryable());

            // Act
            var result = await _analyticsController.Get(panelId);

            // Assert
            Assert.NotNull(result);

            var okResult = result as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Register_PostAnalystics()
        {
            string panelId = "1234567890987654";
            var oneHourElectricityModel = new OneHourElectricityModel
            {
                Id = 1,
                DateTime = DateTime.Now,
                KiloWatt = 100
            };

            // Act
            var result = await _analyticsController.Post(panelId, oneHourElectricityModel);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }
}
