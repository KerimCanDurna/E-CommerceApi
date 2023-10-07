using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.AgregateModels.CategoriModel;
using Domain.AgregateModels.ProductModel;

namespace Core.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");
            builder.Property(b => b.ProductDetail).HasColumnName("ProductDetail").IsRequired();
            builder.Property(b => b.CategoryId).HasColumnName("CategoryId").IsRequired();
            builder.Property(b => b.Price).HasColumnName("Price").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(b => b.Stock).HasColumnName("Stock").IsRequired();


            builder.HasIndex(indexExpression: b => b.Name, name: "UK_Products_Name").IsUnique(); //Ürünün tekilliğini kontrol ediyoruz
                                                                                                 // builder.HasIndex(x => x.PlateNumber).IsUnique();İki yöntem aynıdır fakat yukarıdaki indeksin ismini de tanımlar 

            //builder.HasOne(b => b.SubCategory);
           

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);   // deleteddate in null olup olmadığını kontrol ediyor 
        }
    }
}
