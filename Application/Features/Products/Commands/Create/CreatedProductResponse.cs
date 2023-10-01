﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Create
{
    public class CreatedProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductDetail { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SubCategoryId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}