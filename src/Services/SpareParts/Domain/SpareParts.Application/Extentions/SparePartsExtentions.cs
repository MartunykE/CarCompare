﻿using MongoDB.Bson;
using SpareParts.Application.DTO;
using System.Collections.Generic;
using System.Linq;

namespace SpareParts.Application.Extentions
{
    public static class SparePartsExtentions
    {
        private static string[] defaultSparePartsNames = { "ручник", "комплект грм" };

        public static void AddDefaultSpareParts(this ICollection<SparePartDTO> spareParts)
        {
            if (spareParts.Count() > 0)
            {
                SparePartDTO sparePart;
                foreach (var name in defaultSparePartsNames)
                {
                    sparePart = spareParts.FirstOrDefault(s => s.Name == name);
                    if (sparePart == null)
                    {
                        spareParts.Add(new SparePartDTO { Name = name });
                    }
                }
            }
            else
            {
                foreach (var name in defaultSparePartsNames)
                {
                    spareParts.Add(new SparePartDTO { Name = name });
                }
            }

        }

        public static void GeneratePopertyIds(this VehicleDTO vehicleDTO)
        {
            ObjectId objectId;

            vehicleDTO.Id = ObjectId.TryParse(vehicleDTO.Id, out objectId) ? objectId.ToString() : ObjectId.GenerateNewId().ToString();
            vehicleDTO.VehicleTechSpecification.Id = ObjectId.TryParse(vehicleDTO.VehicleTechSpecification.Id, out objectId) ? objectId.ToString() : ObjectId.GenerateNewId().ToString();

        }
    }
}
