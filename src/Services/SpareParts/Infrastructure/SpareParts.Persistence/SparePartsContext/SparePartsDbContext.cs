using MongoDB.Driver;
using SpareParts.Application.Interfaces;
using SpareParts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Persistence.SparePartsContext
{
    public class SparePartsDbContext : ISparePartsDbContext
    {
        private readonly IMongoDatabase database;
        public SparePartsDbContext(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase("SparePartsDb");
        }

        public IMongoCollection<SparePart> Vehicles => database.GetCollection<SparePart>("SpareParts");

      
    }
}
