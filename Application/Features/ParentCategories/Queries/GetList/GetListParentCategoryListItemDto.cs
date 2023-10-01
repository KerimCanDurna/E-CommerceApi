using Domain.AgregateModels.CategoriModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ParentCategories.Queries.GetList
{
    public class GetListParentCategoryListItemDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SubCategoryName { get; set; }
        public virtual ICollection<ParentCategory> SubCategories { get; set; }



        public DateTime CreatedDate { get; set; }

    }
}
