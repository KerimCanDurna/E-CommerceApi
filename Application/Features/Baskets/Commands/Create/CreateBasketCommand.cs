using Application.Features.Baskets.Dto;
using AutoMapper;
using Domain.AgregateModels.CartModel;
using Domain.AgregateModels.CategoriModel;
using Domain.AgregateModels.ProductModel;
using Domain.AgregateModels.UserModel;
using Domain.IServices.IRepositories;
using Domain.Services.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Baskets.Commands.Create
{
    public class CreateBasketCommand : IRequest<BasketItemDto>
    {


        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string? UserId { get; set; }
        public string? GuestId { get; set; }





        public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, BasketItemDto>
        {

            private readonly IBasketRepository<BasketItem> _basketItemRepository;
            private readonly IMapper _mapper;
            private readonly IBasketRepository<Basket> _basketRepository1;
            private readonly BasketDto _basket;
            private readonly IProductRepository _product;
            private readonly UserManager<User> _userManager;
            private readonly IUserRepository<Guest> _guestrepository;
           



            public CreateBasketCommandHandler(IBasketRepository<BasketItem> basketRepository, IMapper mapper, IBasketRepository<Basket> basketRepository1, BasketDto basket, UserManager<User> userManager, IUserRepository<Guest> guestrepository, IProductRepository product)
            {
                _basketItemRepository = basketRepository;
                _mapper = mapper;

                _basketRepository1 = basketRepository1;
                _basket = basket;
                _userManager = userManager;
                _guestrepository = guestrepository;
                _product = product;
            }

            public async Task<BasketItemDto>? Handle(CreateBasketCommand request, CancellationToken cancellationToken)
            {


                var basketItem = _mapper.Map<BasketItem>(request);
                basketItem.CreatedDate = DateTime.UtcNow;
                basketItem.IsActive = true;

                // Aktif Sepet var mı kontrol etttik
                var search = await _basketRepository1.Where(x=> x.GuestId ==request.GuestId && x.IsActive == true && x.UserId ==request.UserId ).SingleOrDefaultAsync();
               
                if (search == null) // aktif sepet yoksa yeni sepet oluştur
                {
                    _basket.CreatedDate = DateTime.UtcNow;
                    _basket.IsActive = true;

                    var testGuest =await _guestrepository.GetByIdAsync(request.GuestId);
                    var test = await _userManager.FindByIdAsync(request.UserId);
                    if (test == null && testGuest == null)    //Id ler boş gelirse hata ver
                    {
                        throw new Exception("UserId and GuestId is Null or Empty");
                    }

                    else if (test == null)  // UserId null ise Guest Id yi ata
                    {
                        _basket.GuestId = request.GuestId;
                        _basket.UserId = null;
                    }

                    else if (testGuest == null)  // GuestId null ise UserId yi ata
                    {
                        _basket.UserId = request.UserId;
                        _basket.GuestId = null;

                    }
                    var basket =  _mapper.Map<Basket>(_basket);
                    await _basketRepository1.AddAsync(basket);

                }
                var check = await _basketRepository1.Where(x => x.IsActive == true).Where(x => x.UserId == request.UserId && x.GuestId == request.GuestId).FirstOrDefaultAsync();

                basketItem.BasketId = check.Id;

                //Sepetteki ürünü konrol ettik 
                var productControl = await _product.GetAsync(x=>x.Id== request.ProductId);

                if (productControl == null) throw new Exception("ProductId is not found");

                if (productControl.Stock < request.Quantity) throw new Exception("Product Stock is not enoght");


                var control = await _basketItemRepository.Where(x => x.ProductId == basketItem.ProductId && x.IsActive == true).SingleOrDefaultAsync();
                if (control != null)
                {
                    if (basketItem.Quantity < control.Quantity) basketItem.Quantity = control.Quantity;
                    else control.Quantity = basketItem.Quantity;

                    control.UpdatedDate = DateTime.UtcNow;
                    var result = _basketItemRepository.Update(control);
                    var result2 = _mapper.Map<BasketItemDto>(result);

                      return result2;
                }
                else
                    await _basketItemRepository.AddAsync(basketItem);


                var Response = _mapper.Map<BasketItemDto>(basketItem);

                return Response;
            }
        }
    }
}