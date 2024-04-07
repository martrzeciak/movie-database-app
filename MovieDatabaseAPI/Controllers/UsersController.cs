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
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] PaginationParams paginationParams)
        {
            var users = await _unitOfWork.UserRepository.GetMembersAsync(paginationParams);

            Response.AddPaginationHeader(new PaginationHeader(
                users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(username);

            if (user == null) return NotFound();

            return Ok(_mapper.Map<MemberDto>(user));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userName = User.GetUsername();

            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user);
            _unitOfWork.UserRepository.Update(user);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update user");
        }

        [Authorize]
        [HttpPost("add-user-image")]
        public async Task<ActionResult<UserImageDto>> AddeUserImage(IFormFile file)
        {
            var userName = User.GetUsername();

            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            var results = await _imageService.AddImageAsync(file, "user");

            if (results.Error != null) return BadRequest(results.Error.Message);

            var userImage = new UserImage
            {
                ImageUrl = results.SecureUrl.AbsoluteUri,
                PublicId = results.PublicId
            };

            if (user.UserImages.Count == 0) userImage.IsMain = true;

            user.UserImages.Add(userImage);

            if (await _unitOfWork.Complete())
            {
                return CreatedAtAction(nameof(GetUser),
                    new { username = user.UserName },
                    _mapper.Map<UserImageDto>(userImage));
            }

            return BadRequest("Problem adding photo");
        }

        [Authorize]
        [HttpPut("set-main-image/{imageId}")]
        public async Task<ActionResult> SetMainImage(Guid imageId)
        {
            var userName = User.GetUsername();

            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            var userImage = user.UserImages.FirstOrDefault(p => p.Id == imageId);

            if (userImage == null) return NotFound();

            if (userImage.IsMain) return BadRequest("This is already your main image");

            var currentMain = user.UserImages.FirstOrDefault(p => p.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            userImage.IsMain = true;

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to set main photo");
        }

        [Authorize]
        [HttpDelete("delete-user-image/{imageId}")]
        public async Task<ActionResult> DeleteImage(Guid imageId)
        {
            var userName = User.GetUsername();

            var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);

            if (user == null) return NotFound();

            var userImage = user.UserImages.FirstOrDefault(p => p.Id == imageId);

            if (userImage == null) return NotFound();

            if (userImage.IsMain) return BadRequest("You cannot delete your main image");

            if (userImage.PublicId != null)
            {
                var results = await _imageService.DeleteImageAsync(userImage.PublicId);
                if (results.Error != null) return BadRequest(results.Error.Message);
            }

            user.UserImages.Remove(userImage);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete image");
        }
    }
}
