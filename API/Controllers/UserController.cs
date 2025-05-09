using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using MyApi.Application.DTOs.UserDTOs;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;

namespace MyApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> CreateUserDefault(UserCreationDto userDto)
        {
            var user = _mapper.Map<UserEntity>(userDto);
            await _userService.AddUserDefaultAsync(user);
            var createdUserDto = _mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUserDto);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserEditionDto userDto)
        {
            var user = await _userService.GetUserByIdAsync(userDto.Id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(userDto, user);
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
