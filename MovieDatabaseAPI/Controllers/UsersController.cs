using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Extensions;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;

namespace MovieDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] PaginationParams paginationParams)
        {
            var users = await _userRepository.GetMembersAsync(paginationParams);

            Response.AddPaginationHeader(new PaginationHeader(
                users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);

            if (user == null) return NotFound();

            return Ok(_mapper.Map<MemberDto>(user));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userName = User.GetUsername();

            var user = await _userRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}
