using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBProject.API.Data;
using WEBProject.API.Dtos;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApartmentBookRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IApartmentBookRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name ="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForListDto>(user);

            return Ok(userToReturn);
        }
     
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if(await _repo.SaveAll()) 
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpGet("makehost/{id}")]
        public async Task<IActionResult> MakeHost(int id)
        {
            var promoterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var promoter = await _repo.GetUser(promoterId);

            if (promoter.Role != "Admin")
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(id);
            

            if (promoter.Id == id || userFromRepo.Role == "Host")
            {
                return BadRequest();
            }

            userFromRepo.Role = "Host";

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Promoting user {id} failed on save");
        }

        [HttpGet("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var promoterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var promoter = await _repo.GetUser(promoterId);

            if (promoter.Role != "Admin")
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(id);
            
            if (promoter.Id == id || userFromRepo.Role == "Admin" || userFromRepo.IsDeleted == true)
            {
                return BadRequest();
            }

            userFromRepo.IsDeleted = true;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Promoting user {id} failed on save");
        }

        [HttpGet("blockuser/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var promoterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var promoter = await _repo.GetUser(promoterId);

            if (promoter.Role != "Admin")
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(id);

            if (promoter.Id == id || userFromRepo.Role == "Admin" || userFromRepo.IsBlocked == true)
            {
                return BadRequest();
            }

            userFromRepo.IsBlocked = true;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Promoting user {id} failed on save");
        }

        [HttpGet("unblockuser/{id}")]
        public async Task<IActionResult> UnBlockUser(int id)
        {
            var promoterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var promoter = await _repo.GetUser(promoterId);

            if (promoter.Role != "Admin")
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(id);

            if (promoter.Id == id || userFromRepo.Role == "Admin" || userFromRepo.IsBlocked == false)
            {
                return BadRequest();
            }

            userFromRepo.IsBlocked = false;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Promoting user {id} failed on save");
        }
    }
} 