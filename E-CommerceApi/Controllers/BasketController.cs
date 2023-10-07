using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Query.BasketQuery;
using Application.Features.SubCategories.Queries.GetbyId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBasket (string UserId)
        {
            GetBasketQuery query = new() {  UserId = UserId };
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult>AddItemFromBasket([FromBody] CreateBasketCommand addItem)
        {
            var response = await Mediator.Send(addItem);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItemFromBaskey([FromBody] DeleteBasketCommand delete)
        {
            var response = await Mediator.Send(delete);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> BasketToOrder(string Userıd  )
        {
            BasketToQuer getBasketQuery = new() { UserId= Userıd };
            var response = await Mediator.Send(getBasketQuery);
            return Ok(response);
        }
    }
}
