﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Domain.Models
{
    public class SparePart
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public SparePartPrices Prices { get; set; }
    }
}
