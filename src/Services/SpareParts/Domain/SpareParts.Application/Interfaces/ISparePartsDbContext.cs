using MongoDB.Driver;
using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System.Threading.Tasks;

namespace SpareParts.Application.Interfaces
{
    public interface ISparePartsDbContext
    {
        IMongoCollection<Vehicle> Vehicles { get; }
        Task Get();
    }
}
