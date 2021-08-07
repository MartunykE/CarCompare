using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Persistence.Migrations
{
    interface IMigration 
    {
        public int DbVersion { get; }
        public void Up(IMongoDatabase database);
        public void Down(IMongoDatabase database);

    }
}
