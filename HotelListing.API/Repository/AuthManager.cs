using AutoMapper;
using HotelListing.API.Interfaces;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUserDto> _userManager;  

        public AuthManager(IMapper mapper, UserManager<ApiUserDto> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        async Task<IEnumerable<IdentityError>> IAuthManager.Register(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUserDto>(userDto);
            user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }


            return result.Errors;

        }
    }
}
