using Domain.AgregateModels.CategoriModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasData(new Product { Id = 1, CreatedDate = DateTime.UtcNow, Name = "asus Bilgisayar ", SubCategoryId = 1, Price = 1400, Stock = 20 , ProductDetail="i5" },
                new Product { Id = 2, CreatedDate = DateTime.UtcNow, Name = "AsusTablet ", SubCategoryId = 1, Price = 1400, Stock = 20, ProductDetail = "i5" },
                new Product { Id = 3, CreatedDate = DateTime.UtcNow, Name = "vestelBuzdolabı ", SubCategoryId = 2, Price = 1400, Stock = 20, ProductDetail = "vestel" },
                 new Product { Id = 4, CreatedDate = DateTime.UtcNow, Name = "vestelÇamasır makinesi ", SubCategoryId = 2, Price = 1400, Stock = 20, ProductDetail = "vewstel" },
                  new Product { Id = 5, CreatedDate = DateTime.UtcNow, Name = "teflonTava ", SubCategoryId = 3, Price = 1400, Stock = 20, ProductDetail = "teflon" }


                );

        }
    }
}