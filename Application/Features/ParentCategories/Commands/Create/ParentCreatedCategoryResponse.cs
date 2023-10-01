using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ParentCategories.Commands.Create
{
    public class ParentCreatedCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }        

        public DateTime CreatedDate { get; set; }
    }
}
