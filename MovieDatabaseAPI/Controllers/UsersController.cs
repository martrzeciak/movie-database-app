using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.DTOs;
using MovieDatabaseAPI.Entities;
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
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
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

        [Authorize]
        [HttpPost("add-user-image")]
        public async Task<ActionResult<UserImageDto>> UpdateUserPhoto(IFormFile file)
        {
            var userName = User.GetUsername();

            var user = await _userRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            var results = await _photoService.AddPhotoAsync(file);

            if (results.Error != null) return BadRequest(results.Error.Message);

            var userImage = new UserImage
            {
                ImageUrl = results.SecureUrl.AbsoluteUri,
                PublicId = results.PublicId
            };

            if (user.UserImages.Count == 0) userImage.IsMain = true;

            user.UserImages.Add(userImage);

            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetUser),
                    new { username = user.UserName },
                    _mapper.Map<UserImageDto>(userImage));
            }

            return BadRequest("Problem adding photo");
        }

        [Authorize]
        [HttpPut("set-main-image/{imageId}")]
        public async Task<ActionResult> SetMainPhoto(Guid imageId)
        {
            var userName = User.GetUsername();

            var user = await _userRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            var userImage = user.UserImages.FirstOrDefault(p => p.Id == imageId);

            if (userImage == null) return NotFound();

            if (userImage.IsMain) return BadRequest("This is already your main image");

            var currentMain = user.UserImages.FirstOrDefault(p => p.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            userImage.IsMain = true;

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main photo");
        }

        [HttpDelete("delete-user-image/{imageId}")]
        public async Task<ActionResult> DeletePhoto(Guid imageId)
        {
            var userName = User.GetUsername();

            var user = await _userRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            var userImage = user.UserImages.FirstOrDefault(p => p.Id == imageId);

            if (userImage == null) return NotFound();

            if (userImage.IsMain) return BadRequest("You cannot delete your main image");

            if (userImage.PublicId != null)
            {
                var results = await _photoService.DeletePhotoAsync(userImage.PublicId);
                if (results.Error != null) return BadRequest(results.Error.Message);
            }

            user.UserImages.Remove(userImage);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete image");
        }
    }
}
