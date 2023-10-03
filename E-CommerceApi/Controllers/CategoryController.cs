using Application.Features.ParentCategories.Commands.Create;
using Application.Features.ParentCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{


    public class CategoryController : BaseController
    {             


        [HttpPost]
        public async Task<IActionResult> AddParentCategory ([FromBody] ParentCreateCategoryCommand creaateCategoryCommend)
        {
            var response = await Mediator.Send(creaateCategoryCommend);
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddSubCategory([FromBody] SubCreateCategoryCommand creaateCategoryCommend)
        {
            var response = await Mediator.Send(creaateCategoryCommend);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListSubCategoryQuery query = new GetListSubCategoryQuery();
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        
                      
        [HttpDelete]
        public async Task<IActionResult> DeleteSubCategory([FromBody] int id)
        {
            DeleteSubCategoryResponse response = await Mediator.Send(new DeleteSubCategoryCommand { Id = id });

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteParentCategory([FromBody] int id)
        {
            DeleteParentCategoryResponse response = await Mediator.Send(new DeleteParentCategoryCommand { Id = id });

            return Ok(response);
        }
    }
}
     
            







               



