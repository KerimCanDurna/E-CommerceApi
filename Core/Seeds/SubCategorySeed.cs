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
    public class PSubCategorySeed : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {

            builder.HasData(new SubCategory { Id = 1, CreatedDate = DateTime.UtcNow, Name = "Bilgisayar " , ParentCategoryId =2 },
                new SubCategory { Id = 2, CreatedDate = DateTime.UtcNow, Name = "Tablet ", ParentCategoryId = 2 },
                new SubCategory { Id = 3, CreatedDate = DateTime.UtcNow, Name = "Buzdolabı ", ParentCategoryId = 1 },
                 new SubCategory { Id = 4, CreatedDate = DateTime.UtcNow, Name = "Çamasır makinesi ", ParentCategoryId = 1 },
                  new SubCategory { Id = 5, CreatedDate = DateTime.UtcNow, Name = "Tava ", ParentCategoryId = 3 }


                );
                
        }
    }
}
