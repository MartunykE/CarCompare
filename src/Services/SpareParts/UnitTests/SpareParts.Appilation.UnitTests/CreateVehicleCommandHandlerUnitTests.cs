using Moq;
using SpareParts.Application.Features.SparePartsFeatues.Commands;
using SpareParts.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static SpareParts.Application.Features.SparePartsFeatues.Commands.CreateVehicleCommand;
using Serilog;
using MongoDB.Driver;
using SpareParts.Application.IntegrationEvents.Services;
using SpareParts.Application.DTO;
using MongoDB.Bson;
using MongoDB.Driver.Core.Connections;
using SpareParts.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SpareParts.Appilation.UnitTests
{
    public class IInt
    {
        public string GetInt()
        {
            return "";
        }
    }

    public class CreateVehicleCommandHandlerUnitTests
    {
        private readonly Mock<ISparePartsDbContext> sparePartsDbContextMock;
        private readonly Mock<ILogger> loggerMock;
        private readonly Mock<IClientSessionHandle> clientSessionHandleMock;
        private readonly Mock<ISparePartsIntegrationEventService> sparePartsIntegrationEventServiceMock;
        private readonly Mock<IMongoCollection<Vehicle>> vehicleCollectionMock;

        public CreateVehicleCommandHandlerUnitTests()
        {
            sparePartsDbContextMock = new Mock<ISparePartsDbContext>();
            loggerMock = new Mock<ILogger>();
            clientSessionHandleMock = new Mock<IClientSessionHandle>();
            sparePartsIntegrationEventServiceMock = new Mock<ISparePartsIntegrationEventService>();
            vehicleCollectionMock = new Mock<IMongoCollection<Vehicle>>();
        }

        [Fact]
        public async Task Handle_Vehicle_ReturnsMaybeHasNoValueWhenThrowException()
        {
            var fakeCreateVehicleCommand = FakeCreateVehicleCommand();
            var cursorMock = new Mock<IAsyncCursor<Vehicle>>();

            cursorMock.Setup(c => c.Current).Returns(new List<Vehicle>());

            vehicleCollectionMock
                .Setup(vehicles => vehicles.FindAsync(
                    It.IsAny<FilterDefinition<Vehicle>>(), 
                    It.IsAny<FindOptions<Vehicle>>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(cursorMock.Object);

            vehicleCollectionMock.Setup(ctx => ctx.InsertOneAsync(It.IsAny<Vehicle>(), null, default))
                .ThrowsAsync(new NullReferenceException());

            sparePartsDbContextMock.Setup(context => context.Vehicles)
                .Returns(vehicleCollectionMock.Object);

            var createVehicleCommandHandler = new CreateVehicleCommandHandler(
                    sparePartsDbContextMock.Object,
                    loggerMock.Object,
                    sparePartsIntegrationEventServiceMock.Object,
                    clientSessionHandleMock.Object);

            var result = await createVehicleCommandHandler.Handle(fakeCreateVehicleCommand, default);

            Assert.True(result.HasNoValue);
        }


        private CreateVehicleCommand FakeCreateVehicleCommand()
        {
            return new CreateVehicleCommand(new VehicleDTO
            {
                Id = "0001",
                StartProductionYear = DateTime.Now,
                EndProductionYear = DateTime.Now,
                VehicleType = Domain.Models.VehicleType.Car,
                Generation = 0,
                Model = "model",
                ManufacturerName = "ManufacturerName",
                VehicleTechSpecification = new VehicleTechSpecificationDTO
                {
                    Id = "0002",
                    Engine = new EngineDTO
                    {
                        Id = "0003",
                        Name = "Engine",
                        HorsePowers = 50,
                        EngineCapacity = 1.6,
                        Petrol = "Gas",
                    },
                    VehicleId = "0001",
                    GearBox = new GearboxDTO
                    {
                        Id = "0004",
                        GearBoxType = "Auto",
                        Name = "Name",
                        GearsCount = 6,
                    },
                    SpareParts = new List<SparePartDTO>()
                    {
                        new SparePartDTO
                        {
                            Id = "005",
                            Name = "SpaarepartName",
                            Prices = new Domain.Models.SparePartPrices
                            {
                                AveragePrice = 250,
                                MinPrice = 0,
                                MaxPrice = 500
                            },
                        }
                    },
                    AdditionalCharacteristics = new Dictionary<string, string>(),
                }
            });
        }
    }
}
