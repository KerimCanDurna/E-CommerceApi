using Application.Features.Baskets.Dto;
using AutoMapper;
using Domain.AgregateModels.CartModel;
using Domain.IServices.IRepositories;
using Domain.IServices.ISharedIdentity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Baskets.Commands.Delete
{
    public class DeleteBasketCommand : IRequest<BasketItemDto>
    {


        public int ProductId { get; set; }






        public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, BasketItemDto>
        {
            private readonly ISharedIdentityService _sharedIdentityService;
            private readonly IBasketRepository<BasketItem> _basketRepository;
            private readonly IMapper _mapper;
            private readonly IBasketRepository<Basket> _basketRepository1;
            private readonly BasketDto _basket;

            

            public DeleteBasketCommandHandler(IBasketRepository<BasketItem> basketRepository, IMapper mapper, ISharedIdentityService sharedIdentityService, IBasketRepository<Basket> basketRepository1, BasketDto basket)
            {
                _basketRepository = basketRepository;
                _mapper = mapper;
                _sharedIdentityService = sharedIdentityService;
                _basketRepository1 = basketRepository1;
                _basket = basket;

                
            }

            public async Task<BasketItemDto>? Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
            {

                var search = _basketRepository1.Where(x => x.IsActive == true).SingleOrDefault();

                if (search == null) throw new Exception("Have not Active Basket");

                var basketItem = await _basketRepository.GetByIdAsync( request.ProductId);
               
               _basketRepository.Remove(basketItem);               

                
                

                var Response = _mapper.Map<BasketItemDto>(basketItem);

                return Response;
            }
        }
    }
}
