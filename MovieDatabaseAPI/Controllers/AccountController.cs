using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName.ToLower())) 
                return BadRequest("Username is taken");

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email.ToLower()))
                return BadRequest("Email is already registered");

            var user = _mapper.Map<User>(registerDto);
            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            var userDto = new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateTokenAsync(user),
                Gender = user.Gender
            };

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(i => i.UserImages)
                .FirstOrDefaultAsync(x => x.UserName == loginDto.UserName || x.Email == loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username or email");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Invalid password");

            var userDto = new UserDto
            {
                UserName = user.UserName!,
                Token = await _tokenService.CreateTokenAsync(user),
                Gender = user.Gender,
                ImageUrl = user.UserImages.FirstOrDefault(x => x.IsMain)?.ImageUrl,
            };

            return Ok(userDto);
        }
    }
}
