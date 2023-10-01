using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Query.BasketQuery;
using Application.Features.ParentCategories.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{
   [Authorize]
    
    public class BasketController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var getBasketQuery = new GetBasketQuery();
            var response = await Mediator.Send(getBasketQuery);
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
        public async Task<IActionResult> BasketToOrder()
        {
            var getBasketQuery = new GetBasketQuery();
            var response = await Mediator.Send(getBasketQuery);
            return Ok(response);
        }
    }
}
