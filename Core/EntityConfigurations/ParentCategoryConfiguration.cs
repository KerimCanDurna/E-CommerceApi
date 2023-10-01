using Domain.AgregateModels.CategoriModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EntityConfigurations
{
    public class ParentCategoryConfiguration : IEntityTypeConfiguration<ParentCategory>
    {
        public void Configure(EntityTypeBuilder<ParentCategory> builder)
        {
            builder.ToTable("ParentCategories").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired().UseIdentityColumn();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");


            builder.HasIndex(indexExpression: b => b.Name).IsUnique(); //Ürünün tekilliğini kontrol ediyoruz
                                                                       // builder.HasIndex(x => x.PlateNumber).IsUnique();İki yöntem aynıdır fakat yukarıdaki indeksin ismini de tanımlar 

            builder.HasMany(c => c.SubCategories);

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);   // deleteddate in null olup olmadığını kontrol ediyor 
        }
    }
}

