using Domain.AgregateModels.CategoriModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Seeds
{
    public class PSubCategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasData(new Category { Id = 1, CreatedDate = DateTime.UtcNow, Name = "Elektronik " },
             new Category { Id = 2, CreatedDate = DateTime.UtcNow, Name = "BeyazEşya " },
             new Category { Id = 3, CreatedDate = DateTime.UtcNow, Name = "Mutfak " },
             new Category { Id = 4, CreatedDate = DateTime.UtcNow, Name = "Bilgisayar ", ParentCategoryId = 1 },
                new Category { Id = 5, CreatedDate = DateTime.UtcNow, Name = "Tablet ", ParentCategoryId = 1 },
                new Category { Id = 6, CreatedDate = DateTime.UtcNow, Name = "Buzdolabı ", ParentCategoryId = 2 },
                 new Category { Id = 7, CreatedDate = DateTime.UtcNow, Name = "Çamasır makinesi ", ParentCategoryId = 2 },
                  new Category { Id = 8, CreatedDate = DateTime.UtcNow, Name = "Tava ", ParentCategoryId = 3 },
                 new Category { Id = 9, CreatedDate = DateTime.UtcNow, Name = "NoteBook " , ParentCategoryId = 4 },
               new Category { Id = 10, CreatedDate = DateTime.UtcNow, Name = "MasaÜstü "  ,ParentCategoryId=4}



                );

        }
    }
}
