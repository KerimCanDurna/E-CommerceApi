using Domain.AgregateModels.CategoriModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetList
{
    public class CategoryDto
    {

        public string? ParentCategoryName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryDto>? SubCategories { get; set; } = new List<CategoryDto>();
        public DateTime CreatedDate { get; set; }

    }
}
