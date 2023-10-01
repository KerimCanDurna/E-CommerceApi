using Domain.AgregateModels.CategoriModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Seeds
{
    public class ParetnCategorySeed : IEntityTypeConfiguration<ParentCategory>
    {
        public void Configure(EntityTypeBuilder<ParentCategory> builder)
        {

            builder.HasData(new ParentCategory { Id = 1, CreatedDate = DateTime.UtcNow, Name = "Beyaz Eşya " },
                new ParentCategory { Id = 2, CreatedDate = DateTime.UtcNow, Name = "Elektronik " },
                new ParentCategory { Id = 3, CreatedDate = DateTime.UtcNow, Name = "Mutfak " });
        }
    }
}
