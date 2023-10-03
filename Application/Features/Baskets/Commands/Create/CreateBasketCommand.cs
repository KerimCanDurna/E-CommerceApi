using Application.Features.Baskets.Dto;
using Application.Features.Products.Commands.Create;
using AutoMapper;
using Domain.AgregateModels.CartModel;
using Domain.AgregateModels.UserModel;
using Domain.IServices.IRepositories;
using Domain.IServices.ISharedIdentity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Baskets.Commands.Create
{
    public class CreateBasketCommand : IRequest<BasketItemDto>
    {


        public int ProductId { get; set; }
        public int Quantity { get; set; }





        public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, BasketItemDto>
        {
            private readonly ISharedIdentityService _sharedIdentityService;
            private readonly IBasketRepository<BasketItem> _basketRepository;
            private readonly IMapper _mapper;
            private readonly IBasketRepository<Basket> _basketRepository1;
            private readonly BasketDto _basket;
            private readonly UserManager<User> _userManager;



            public CreateBasketCommandHandler(IBasketRepository<BasketItem> basketRepository, IMapper mapper, ISharedIdentityService sharedIdentityService, IBasketRepository<Basket> basketRepository1, BasketDto basket, UserManager<User> userManager)
            {
                _basketRepository = basketRepository;
                _mapper = mapper;
                _sharedIdentityService = sharedIdentityService;
                _basketRepository1 = basketRepository1;
                _basket = basket;
                _userManager = userManager;
            }

            public async Task<BasketItemDto>? Handle(CreateBasketCommand request, CancellationToken cancellationToken)
            {


                var basketItem = _mapper.Map<BasketItem>(request);
                basketItem.CreatedDate = DateTime.UtcNow;
                basketItem.IsActive = true;
                 
                // Aktif Sepet var mı kontrol etttik
                var search = _basketRepository1.Where(x => x.IsActive == true).SingleOrDefault();
                if (search == null) // aktif sepet yoksa yeni sepet oluştur
                {
                    _basket.CreatedDate = DateTime.UtcNow;
                    _basket.IsActive = true;

                     
                    // var userid = _sharedIdentityService.GetUserId; // misafir oturumu mu yoksa uye oturumu mu token uzerinden kontrol eetik 
                    if (userid == null)
                    {
                        _basket.UserId = null;
                        var basket1 = _mapper.Map<Basket>(_basket);
                        _basketRepository1.AddAsync(basket1);
                    }
                    _basket.UserId = userid.ToString();


                    var basket = _mapper.Map<Basket>(_basket);
                    _basketRepository1.AddAsync(basket);
                                       
                }
               
                var check = _basketRepository1.Where(x => x.IsActive == true).SingleOrDefault();
                basketItem.BasketId = check.Id ;

                //Sepetteki ürünü konrol ettik 
                var control = _basketRepository.Where(x=>x.ProductId == basketItem.ProductId && x.IsActive== true).SingleOrDefault();
                if (control != null)
                {
                    if(basketItem.Quantity < control.Quantity) basketItem.Quantity = control.Quantity;
                    else control.Quantity = basketItem.Quantity;
                    
                        control.UpdatedDate = DateTime.UtcNow;
                        var result = _basketRepository.Update(control);
                        var result2 = _mapper.Map<BasketItemDto>(result);
                    

                    return result2;
                }
                else
                await _basketRepository.AddAsync(basketItem);


                var Response = _mapper.Map<BasketItemDto>(basketItem);

                return Response;
            }
        }
    }
}