﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerceStaticWebApp.Shared
{
    public class ProductSearchResult
    {
        public List<Product> Products { get; set; } = new List<Product> { new Product { Id = 1, } };
        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
