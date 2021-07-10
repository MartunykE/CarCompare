using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Moq;
using SpareParts.Application.Interfaces;
using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpareParts.Appilation.UnitTests
{
    public class UnitTest1
    {

        private readonly Mock<IAsyncCursor<Vehicle>> cursor;
        private readonly Mock<IMongoCollection<Vehicle>> vehicleCollectionMock;
        private readonly List<Vehicle> vehicles;
        private readonly Mock<ISparePartsDbContext> sparePartsDbContextMock;


        public UnitTest1()
        {
            sparePartsDbContextMock = new Mock<ISparePartsDbContext>();
            cursor = new Mock<IAsyncCursor<Vehicle>>();
            vehicleCollectionMock = new Mock<IMongoCollection<Vehicle>>();
            
            vehicles = new List<Vehicle>();
            var vehicle2 = new Vehicle
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

            };
            vehicles.Add(vehicle2);

            cursor.Setup(c => c.Current).Returns(vehicles);
            cursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            cursor.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true).ReturnsAsync(false);

        }


        [Fact]
        public async Task Test1()
        {
           
        }

            
    }
}
