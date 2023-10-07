using Domain.AgregateModels.CategoriModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.AgregateModels.ProductModel;

namespace Core.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasData(new Product { Id = 1, CreatedDate = DateTime.UtcNow, Name = "asus Bilgisayar ", CategoryId = 10, Price = 1400, Stock = 20 , ProductDetail="i5" },
                new Product { Id = 2, CreatedDate = DateTime.UtcNow,IsActive = true, Name = "AsusTablet ", CategoryId = 9, Price = 1400, Stock = 20, ProductDetail = "i5" },
                new Product { Id = 3, CreatedDate = DateTime.UtcNow, IsActive = true, Name = "vestelBuzdolabı ", CategoryId = 6, Price = 1400, Stock = 20, ProductDetail = "vestel" },
                 new Product { Id = 4, CreatedDate = DateTime.UtcNow,IsActive = true, Name = "vestelÇamasır makinesi ", CategoryId = 7, Price = 1400, Stock = 20, ProductDetail = "vewstel" },
                  new Product { Id = 5, CreatedDate = DateTime.UtcNow, IsActive = true, Name = "teflonTava ", CategoryId = 8, Price = 1400, Stock = 20, ProductDetail = "teflon" }


                );

        }
    }
}