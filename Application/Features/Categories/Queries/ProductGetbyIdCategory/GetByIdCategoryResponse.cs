using Application.Features.Categories.Queries.GetList;
using Application.Features.Products.Commands.Create;
using Domain.AgregateModels.CategoriModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Features.SubCategories.Queries.GetbyId
{
    public class GetByIdCategoryResponse
    {
        public string? ParentCategoryName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CreatedProductResponse>? Products { get; set; } = new List<CreatedProductResponse>();
        public ICollection<GetByIdCategoryResponse>? SubCategories { get; set; } = new List<GetByIdCategoryResponse>();
       

       
    }
}
