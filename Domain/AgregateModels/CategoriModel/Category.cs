using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AgregateModels.CategoriModel
{
    public class Category :BaseEntity
    {
        public int? ParentCategoryId { get; set; }

        public virtual Category? UstCategory { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Category>? AltKategory { get; set; }
    }
}
