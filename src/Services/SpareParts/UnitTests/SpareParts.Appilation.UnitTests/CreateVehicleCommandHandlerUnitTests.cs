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
using SpareParts.Domain.Models.VehicleTechSpecification;
using System.Linq.Expressions;
using System.Linq;
using SpareParts.Application.Mapper;

namespace SpareParts.Appilation.UnitTests
{
    public class CreateVehicleCommandHandlerUnitTests
    {
        private readonly Mock<ISparePartsDbContext> sparePartsDbContextMock;
        private readonly Mock<ILogger> loggerMock;
        private readonly Mock<IClientSessionHandle> clientSessionHandleMock;
        private readonly Mock<ISparePartsIntegrationEventService> sparePartsIntegrationEventServiceMock;
        private readonly Mock<IMongoCollection<Vehicle>> vehicleCollectionMock;
        private readonly List<Vehicle> fakeVehiclesList;

        public CreateVehicleCommandHandlerUnitTests()
        {
            loggerMock = new Mock<ILogger>();
            clientSessionHandleMock = new Mock<IClientSessionHandle>();
            sparePartsIntegrationEventServiceMock = new Mock<ISparePartsIntegrationEventService>();
            vehicleCollectionMock = new Mock<IMongoCollection<Vehicle>>();


            sparePartsDbContextMock = new Mock<ISparePartsDbContext>();
            fakeVehiclesList = FakeVehiclesList();

        }

        [Fact]
        public async Task Handle_CreateVehicleCommand_ReturnsMaybeHasNoValue_WhenThrowException()
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
                .ThrowsAsync(new Exception());

            sparePartsDbContextMock.Setup(context => context.Vehicles)
                .Returns(vehicleCollectionMock.Object);

            var vehicleMapper = new VehicleMapper();
            var createVehicleCommandHandler = new CreateVehicleCommandHandler(
                    sparePartsDbContextMock.Object,
                    loggerMock.Object,
                    sparePartsIntegrationEventServiceMock.Object,
                    clientSessionHandleMock.Object,
                    vehicleMapper);

            var result = await createVehicleCommandHandler.Handle(fakeCreateVehicleCommand, default);

            Assert.True(result.HasNoValue);
        }

        [Fact]
        public async Task Handle_CreateVehicleCommand_ReturnsVehicleId()
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

            sparePartsDbContextMock.Setup(context => context.Vehicles)
                .Returns(vehicleCollectionMock.Object);

            var vehicleMapper = new FakeVehicleMapper();
          
            var createVehicleCommandHandler = new CreateVehicleCommandHandler(
                    sparePartsDbContextMock.Object,
                    loggerMock.Object,
                    sparePartsIntegrationEventServiceMock.Object,
                    clientSessionHandleMock.Object,
                    vehicleMapper);

            var result = await createVehicleCommandHandler.Handle(fakeCreateVehicleCommand, default);

            var expectedId = "210e1e7b45b69bde39f423be";

            Assert.Equal(result.Value, expectedId.ToString());
        }



        //[Fact]
        //public async Task Handle_CreateVehicleCommand_ReturnsFoundedVehcleId_WhenVehicleExists()
        //{
        //    var vehicleId = "210e1e7b45b69bde39f423be";

        //    var list = FakeVehiclesList();

        //    var cursor = new Mock<IAsyncCursor<Vehicle>>();
        //    //cursor.Setup(c => c.Current = list)

        //    //vehicleCollectionMock.As<IAsyncCursorSource<Vehicle>>().Setup(m => m.ToCursorAsync(default)).Returns(list); ;

        //    //vehicleCollectionMock.As<IQueryable<Vehicle>>().Setup(c => c.Provider).Returns(list.Provider);
        //    //vehicleCollectionMock.As<IQueryable<Vehicle>>().Setup(c => c.Expression).Returns(list.Expression);
        //    //vehicleCollectionMock.As<IQueryable<Vehicle>>().Setup(c => c.ElementType).Returns(list.ElementType);
        //    //vehicleCollectionMock.As<IQueryable<Vehicle>>().Setup(c => c.GetEnumerator()).Returns(list.GetEnumerator);

        //    sparePartsDbContextMock.Setup(context => context.Vehicles)
        //        .Returns(vehicleCollectionMock.Object);

        //    var a = await sparePartsDbContextMock.Object.Vehicles.FindAsync(Builders<Vehicle>.Filter.Empty) ;

        //    var createVehicleCommandHandler = new CreateVehicleCommandHandler(
        //          sparePartsDbContextMock.Object,
        //          loggerMock.Object,
        //          sparePartsIntegrationEventServiceMock.Object,
        //          clientSessionHandleMock.Object);



        //    var fakeCreateVehicleCommand = FakeCreateVehicleCommand();

        //    var result = await createVehicleCommandHandler.Handle(fakeCreateVehicleCommand, default);

        //    Assert.True(result.HasValue);
        //    Assert.Equal(vehicleId, result.Value);
        //}

        private List<Vehicle> FakeVehiclesList()
        {
            return new List<Vehicle>()
            {
                new Vehicle
                {
                    Id = ObjectId.Parse("53bc56f60fc6bc030630915d"),
                    ManufacturerName = "Audi",
                    Model = "A4",
                    Generation = 1,
                    EndProductionYear = DateTime.Parse("01/01/2015"),
                    StartProductionYear = DateTime.Parse("01/01/2010"),
                    VehicleTechSpecifications = new List<VehicleTechSpecification>()
                    {
                        new VehicleTechSpecification
                        {
                            Id = ObjectId.Parse("1903ec035e8deb4b17c4bc79"),
                            Engine = new Engine
                            {
                                Id = ObjectId.Parse("b10cb720f8ecd06f8321bf24"),
                                Name = "TSI",
                                EngineCapacity = 2,
                                HorsePowers = 150,
                                Petrol = "Gas"
                            },
                            GearBox = new GearBox
                            {
                                Id = ObjectId.Parse("62f268986838726182715979"),
                                Name = "DSG",
                                GearBoxType = "Auto",
                                GearsCount = 6,
                            },
                            LastSparePartsPricesUpdateDate = DateTime.Now,
                            SpareParts = new List<SparePart>(),
                            AdditionalCharacteristics = new Dictionary<string,string>(),
                        }
                    }

                },
                new Vehicle
                {
                    Id = ObjectId.Parse("210e1e7b45b69bde39f423be"),
                    ManufacturerName = "BMW",
                    Model = "328",
                    Generation = 1,
                    EndProductionYear = DateTime.Parse("01/01/2015"),
                    StartProductionYear = DateTime.Parse("01/01/2010"),
                    VehicleTechSpecifications = new List<VehicleTechSpecification>()
                    {
                        new VehicleTechSpecification
                        {
                            Id = ObjectId.Parse("03d3834bdb61abb4b958f8d7"),
                            Engine = new Engine
                            {
                                Id = ObjectId.Parse("fd1d8774bbb6a3d392daacb5"),
                                Name = "TSI",
                                EngineCapacity = 2,
                                HorsePowers = 150,
                                Petrol = "Gas"
                            },
                            GearBox = new GearBox
                            {
                                Id = ObjectId.Parse("7a73fb3ce67202dfbcbff51c"),
                                Name = "DSG",
                                GearBoxType = "Auto",
                                GearsCount = 6,
                            },
                            LastSparePartsPricesUpdateDate = DateTime.Now,
                            SpareParts = new List<SparePart>(),
                            AdditionalCharacteristics = new Dictionary<string,string>(),
                        }
                    }
                }
            };
        }

        private CreateVehicleCommand FakeCreateVehicleCommand()
        {
            return new CreateVehicleCommand(new VehicleDTO
            {
                Id = null,
                StartProductionYear = DateTime.Now,
                EndProductionYear = DateTime.Now,
                Generation = 1,
                Model = "328",
                ManufacturerName = "BMW",
                VehicleTechSpecification = new VehicleTechSpecificationDTO
                {
                    Id = null,
                    Engine = new EngineDTO
                    {
                        Id = null,
                        Name = "Engine",
                        HorsePowers = 50,
                        EngineCapacity = 1.6,
                        Petrol = "Gas",
                    },
                    GearBox = new GearboxDTO
                    {
                        Id = null,
                        GearBoxType = "Auto",
                        Name = "Name",
                        GearsCount = 6,
                    },
                    SpareParts = new List<SparePartDTO>()
                    {
                        new SparePartDTO
                        {
                            Id = null,
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

        class FakeVehicleMapper : VehicleMapper
        {
            public override Vehicle MapToVehicle(VehicleDTO vehicleDTO)
            {
                var mapper = new VehicleMapper();
                var vehicleId = "210e1e7b45b69bde39f423be";
                vehicleDTO.Id = vehicleId;

                return mapper.MapToVehicle(vehicleDTO);
            }
        }
    }
}
