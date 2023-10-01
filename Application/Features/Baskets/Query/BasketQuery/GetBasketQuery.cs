using Application.Features.Baskets.Dto;
using AutoMapper;
using Core.Repositories;
using Domain.AgregateModels.CartModel;
using Domain.IServices.IRepositories;
using Domain.IServices.ISharedIdentity;
using Domain.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Application.Features.Baskets.Query.BasketQuery
{
    public class GetBasketQuery : IRequest<BasketDto>
    {
        public string UserId { get; set; }


        public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDto>
        {
            private readonly IMapper _mapper;
            private readonly IBasketRepository<Basket> _basketRepository;
            private readonly ISharedIdentityService _sharedIdentityService;

            public GetBasketQueryHandler(IMapper mapper, IBasketRepository<Basket> basketRepository)
            {
                _mapper = mapper;
                _basketRepository = basketRepository;
            }


            public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
            {
                var basket = await _basketRepository.Where(x=>x.UserId ==_sharedIdentityService.GetUserId).ToListAsync();

                var result = _mapper.Map<BasketDto>(basket);
                return result;
            }
        }
    }
}

