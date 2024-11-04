﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Shared.Dtos.CreateDtos
{
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

}
