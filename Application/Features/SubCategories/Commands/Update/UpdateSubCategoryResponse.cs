using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubCategories.Commands.Update
{
    public class UpdateSubCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
