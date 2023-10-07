using Application.Features.Baskets.Dto;
using AutoMapper;
using Core.Repositories;
using Domain.AgregateModels.CartModel;
using Domain.IServices.IRepositories;

using Domain.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Application.Features.Baskets.Query.BasketQuery
{
    public class GetBasketQuery : IRequest<List<BasketDto>>
    {
        public string UserId { get; set; }


        public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, List<BasketDto>>
        {
            private readonly IMapper _mapper;
            private readonly IBasketRepository<Basket> _basketRepository;
          

            public GetBasketQueryHandler(IMapper mapper, IBasketRepository<Basket> basketRepository)
            {
                _mapper = mapper;
                _basketRepository = basketRepository;
            }


            public async Task<List<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
            {

                var basket = await _basketRepository.Where(x=>x.UserId ==request.UserId || x.GuestId==request.UserId).ToListAsync();
                
               

                var result = _mapper.Map<List<BasketDto>>(basket);
                return result;
            }
        }
    }
}

