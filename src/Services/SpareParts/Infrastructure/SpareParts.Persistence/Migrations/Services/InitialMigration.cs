using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Persistence.Migrations.Services
{
    class InitialMigration : IMigration
    {
        public int DbVersion => 1;

        public void Down(IMongoDatabase database)
        {
            throw new NotImplementedException();
        }

        public void Up(IMongoDatabase database)
        {
            var dbInfoCollection = database.GetCollection<DbInfo>("DbInfo");

            dbInfoCollection.InsertOne(new DbInfo { Version = DbVersion });
        }
    }
}
