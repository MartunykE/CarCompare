using MongoDB.Driver;
using SpareParts.Domain.Models;
using System.Threading.Tasks;

namespace SpareParts.Application.Interfaces
{
    public interface ISparePartsDbContext
    {
        IMongoCollection<SparePart> Vehicles { get; }
    }
}
