using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.ErrorsHandle;
using OrderManagementSystem.Data.Entities.IdentityModels;
using OrderManagementSystem.Services.Dtos;
using OrderManagementSystem.Services.TokenServices;

namespace OrderManagementSystem.API.Controllers
{

    public class UsersController : BaseController
    {
        private readonly UserManager<AppCustomer> _userManager;
        private readonly SignInManager<AppCustomer> _signInManager;
        private readonly ITokenService _token;

        public UsersController(UserManager<AppCustomer> userManager, SignInManager<AppCustomer> signInManager, ITokenService token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        public async Task<ActionResult<UserDto>> Register([FromForm] ReqgisterOrLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorApiResponse(400));
            AppCustomer user = new AppCustomer
            {
                Email = dto.Email,
                UserName = dto.Email.Split('@')[0],
            };
            var result = await _userManager.CreateAsync(user, dto.PassWord);
            await _userManager.AddToRoleAsync(user, "Customer");
            if (!result.Succeeded)
                return BadRequest(new ErrorApiResponse(400));
            UserDto returnedData = new UserDto
            {
                Email = dto.Email,
                Name = user.UserName,
                Token = await _token.GenerateToken(user, _userManager)

            };
            return Ok(returnedData);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(ReqgisterOrLoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                return Unauthorized(new ErrorApiResponse(401, "Not Found Email Create One"));
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.PassWord, false);
            if (!result.Succeeded)
                return Unauthorized(new ErrorApiResponse(401));

            return Ok(new UserDto
            {
                Email = dto.Email,
                Name = user.UserName,
                Token = await _token.GenerateToken(user, _userManager)
            });


        }


       

    }
}
