using Domain.AgregateModels.ProductModel;
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

        public virtual Category? ParentCategory { get; set; }
        public ICollection<Category>? SubCategories { get; set; } = new List<Category>();
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
