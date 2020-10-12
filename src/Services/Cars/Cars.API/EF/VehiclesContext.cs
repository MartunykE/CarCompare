using Cars.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.API.EF
{
    public class VehiclesContext : DbContext
    {
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<Manufacturer> Manufacturers { get; set; }
        public VehiclesContext(DbContextOptions options) : base(options)
        {

        }

    }
}
