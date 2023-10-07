using Domain.AgregateModels.CategoriModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.AgregateModels.UserModel;

namespace Core.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.Surname).HasColumnName("Surname").IsRequired();   
            

            builder.Property(b => b.City).HasColumnName("City").IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

           

            
        }
    }
    
}
