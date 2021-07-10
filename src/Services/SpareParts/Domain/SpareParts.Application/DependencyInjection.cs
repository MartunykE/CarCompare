using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using SpareParts.Application.Mapper;

namespace SpareParts.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddTransient<VehicleMapper>();
        }
    }
}
