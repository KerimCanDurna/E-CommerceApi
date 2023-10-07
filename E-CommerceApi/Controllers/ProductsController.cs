using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetbyId;
using Application.Features.Products.Queries.GetList;
using Domain.AgregateModels.CategoriModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace E_CommerceApi.Controllers
{

    public class ProductsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductCommand creaateProductCommend)
        {
          var response =  await Mediator.Send(creaateProductCommend);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromHeader] int categoryID)
        {
            GetListProductQuery query = new() { CategoryId = categoryID };
            var  response = await Mediator.Send(query);
            return Ok(response);
        }

        

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateproductCommand)
        {
            UpdateProductResponse response = await Mediator.Send(updateproductCommand);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete( int id)
        {
            DeleteProductResponse response = await Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok(response);
        }
    }
}
