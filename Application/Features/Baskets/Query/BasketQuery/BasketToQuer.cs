using Application.Features.Baskets.Dto;
using AutoMapper;
using Domain.AgregateModels.CartModel;
using Domain.IServices.IRepositories;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Baskets.Query.BasketQuery
{
    public class BasketToQuer : IRequest<List<BasketDto>>
    {
        public string UserId { get; set; }
        


        public class BasketToQuerHandler : IRequestHandler<BasketToQuer, List<BasketDto>>
        {
            private readonly IMapper _mapper;
            private readonly IBasketRepository<Basket> _basketRepository;
           

            public BasketToQuerHandler(IMapper mapper, IBasketRepository<Basket> basketRepository)
            {
                _mapper = mapper;
                _basketRepository = basketRepository;
            }


            public async Task<List<BasketDto>> Handle(BasketToQuer request, CancellationToken cancellationToken)
            {
                var basket = await _basketRepository.Where(x => x.UserId == request.UserId  && x.IsActive==true).SingleOrDefaultAsync();
                if (basket == null) { throw new Exception("Basket is not found"); }

                basket.IsActive = false;
                var order = _basketRepository.Update(basket);
                

                var result = _mapper.Map<List<BasketDto>>(basket);
                return result;
            }
        }
    }
}
