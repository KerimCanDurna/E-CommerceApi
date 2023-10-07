using Application.Features.Baskets.Dto;
using Application.Features.TokenIdentity.Dto;
using Application.TokenService.Dto;
using AutoMapper;
using Core;
using Core.Repositories;
using Domain.AgregateModels.CartModel;
using Domain.AgregateModels.UserModel;
using Domain.IServices.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Application.Features.TokenIdentity.TokenService
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository<Guest> _userRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IBasketRepository<Basket> _basbRepository;
        private readonly IBasketRepository<BasketItem> _basbItemRepository;

        public UserService(UserManager<User> userManager, IMapper mapper, IUserRepository<Guest> userRepository, AppDbContext appDbContext, Domain.IServices.IRepositories.IBasketRepository<Basket> basbRepository, IBasketRepository<BasketItem> basbItemRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _appDbContext = appDbContext;
            _basbRepository = basbRepository;
            _basbItemRepository = basbItemRepository;
        }

        public async Task<Response<GuestLoginDto>> LoginGuest(string loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userRepository.GetByIdAsync(loginDto);


            if (user == null)
            {
                var guest = new Guest { Id = loginDto };
                _userRepository.AddAsync(guest);

                return Response<GuestLoginDto>.Success(_mapper.Map<GuestLoginDto>(guest), 200);
            }



            return Response<GuestLoginDto>.Success(_mapper.Map<GuestLoginDto>(user), 200);
        }


        public async Task<Response<UserDto>> LoginUser(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);  // Gelen maile ait user çekildi

            if (user == null) throw new Exception("Mail or Password Wrong");

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new Exception("Password is wrong");

            }
            var control = await _basbRepository.Where(x => x.GuestId == user.GuestId && x.IsActive == true).SingleOrDefaultAsync();  // user ın guestId sine ait olan sepet var mı 
            if (control == null)
            {
                return Response<UserDto>.Success(_mapper.Map<UserDto>(user), 200);
            }
            else   //varsa
            {
                var activeBasketControl = await _basbRepository.Where(x => x.UserId == user.Id && x.IsActive == true).SingleOrDefaultAsync();  //user ıd ye ait basket var mı 
                if (activeBasketControl == null)   //yoksa
                {
                    control.UserId = user.Id;   // guestId ye ait sepetin userId sini ver
                    control.GuestId = null;
                    control.UpdatedDate = DateTime.UtcNow;
                    _basbRepository.Update(control);

                    return Response<UserDto>.Success(_mapper.Map<UserDto>(user), 200);
                }
                else
                {
                    var GuestBasketId = control.Id;
                    var userBasketId = activeBasketControl.Id;

                    var BasketItemControl = await _basbItemRepository.Where(x => x.BasketId == userBasketId && x.IsActive == true).ToListAsync();
                    var basketGuestControl = await _basbItemRepository.Where(x => x.BasketId == GuestBasketId && x.IsActive == true).ToListAsync();

                    foreach (var item in BasketItemControl)
                    {
                        var productControl = basketGuestControl.FirstOrDefault(x => x.ProductId == item.ProductId);
                        if (productControl != null)
                        {
                            if (item.Quantity < productControl.Quantity)
                            {
                                item.Quantity = productControl.Quantity;
                                item.UpdatedDate = DateTime.UtcNow;
                                _basbItemRepository.Update(item);

                            }
                        }
                        else
                        {
                            productControl.UpdatedDate = DateTime.UtcNow;
                            _basbItemRepository.Update(productControl);
                        }
                    }
                }
                return Response<UserDto>.Success(_mapper.Map<UserDto>(user), 200);

            }
        }

   

        

    }

}
