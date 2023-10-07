using Domain.AgregateModels.CategoriModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EntityConfigurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired().UseIdentityColumn();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.ParentCategoryId).HasColumnName("ParentCategoryId");
            builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(indexExpression: b => b.Name).IsUnique(); //Ürünün tekilliğini kontrol ediyoruz
                                                                       // builder.HasIndex(x => x.PlateNumber).IsUnique();İki yöntem aynıdır fakat yukarıdaki indeksin ismini de tanımlar 

            builder.HasOne(x => x.ParentCategory);
            builder.HasMany(x => x.Products);
            

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);   // deleteddate in null olup olmadığını kontrol ediyor 
        }
    }
}


