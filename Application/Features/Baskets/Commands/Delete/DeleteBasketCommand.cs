using Application.Features.Baskets.Dto;
using AutoMapper;
using Domain.AgregateModels.CartModel;
using Domain.IServices.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Baskets.Commands.Delete
{
    public class DeleteBasketCommand : IRequest<BasketItemDto>
    {


        public int ProductId { get; set; }
        public string UserId { get; set; }






        public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, BasketItemDto>
        {
           
            private readonly IBasketRepository<BasketItem> _basketRepository;
            private readonly IMapper _mapper;
            private readonly IBasketRepository<Basket> _basketRepository1;
            private readonly BasketDto _basket;

            

            public DeleteBasketCommandHandler(IBasketRepository<BasketItem> basketRepository, IMapper mapper, IBasketRepository<Basket> basketRepository1, BasketDto basket)
            {
                _basketRepository = basketRepository;
                _mapper = mapper;
                
                _basketRepository1 = basketRepository1;
                _basket = basket;

                
            }

            public async Task<BasketItemDto>? Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
            {

                var search = _basketRepository1.Where(x => x.IsActive == true && x.UserId==request.UserId ||x.GuestId== request.UserId).SingleOrDefault();

                if (search == null) throw new Exception("Have not Active Basket");

                var basketItem = await _basketRepository.Where( x=>x.ProductId == request.ProductId && x.BasketId == search.Id).SingleOrDefaultAsync();

                if (basketItem == null) throw new Exception("Basket has not productID");
               
               _basketRepository.Remove(basketItem);                               
                

                var Response = _mapper.Map<BasketItemDto>(basketItem);

                return Response;
            }
        }
    }
}
