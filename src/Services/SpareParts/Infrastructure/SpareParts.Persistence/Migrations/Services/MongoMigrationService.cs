using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace SpareParts.Persistence.Migrations.Services
{
    class DbInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Version { get; set; }
    }


    public class MongoMigrationService
    {
        private readonly IMongoClient mongoClient;

        public MongoMigrationService(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
        }

        public void SetupDatabase(string databaseName)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            CreateDatabaseInfo(database);
            ApplyMigrations(database);
        }

        private IEnumerable<IMigration> MigrationsList => new List<IMigration>
        {
            new VehicleModelEngineGearboxIndexesMigration()
        }
       .OrderBy(m => m.DbVersion);

        private void CreateDatabaseInfo(IMongoDatabase database)
        {
            var dbInfoCollection = database.GetCollection<DbInfo>("DbInfo");

            var dbInfos = dbInfoCollection.Find(new BsonDocument());

            if (dbInfos.CountDocuments() == 0)
            {
                var initMigration = new InitialMigration();
                initMigration.Up(database);
            }
        }

        private void ApplyMigrations(IMongoDatabase database)
        {
            var dbInfoCollection = database.GetCollection<DbInfo>("DbInfo");

            DbInfo dbInfo;
            var dbInfos = dbInfoCollection.Find(new BsonDocument());
            if (dbInfos.CountDocuments() == 0)
            {
                throw new InvalidOperationException($"DB info wasn`t found in database. Create 'DbInfo' collection with 1 {typeof(DbInfo)} document inside first");
            }
            else
            {
                dbInfo = dbInfos.First();
            }

            var initialDbVersion = dbInfo.Version;

            foreach (var migration in MigrationsList)
            {
                if (dbInfo.Version < migration.DbVersion)
                {
                    migration.Up(database);
                    dbInfo.Version = migration.DbVersion;
                }
            }

            if (dbInfo.Version > initialDbVersion)
            {
                dbInfoCollection.ReplaceOne(d => d.Id == dbInfo.Id, dbInfo);
            }
        }
        
       
    }
}
