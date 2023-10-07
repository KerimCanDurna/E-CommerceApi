using Application.Features.Categories.Queries.GetList;
using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Queries.GetbyId;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{


    public class CategoryController : BaseController
    {             
                        
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryCommand creaateCategoryCommend)
        {
            var response = await Mediator.Send(creaateCategoryCommend);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListCategoryQuery query = new GetListCategoryQuery();
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetListID(int Id )
        {
            GetByIdCategoryQuery query = new() {Id =Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody] int id)
        {
            DeleteCategoryResponse response = await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return Ok(response);
        }

       
    }
}
     
            







               



