using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBProject.API.Data;

namespace WEBProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentBookRepository _repo;
        private readonly IMapper _mapper;
        public ApartmentsController(IApartmentBookRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]

        public async Task<IActionResult> GetActiveApartments()
        { 
            var apartments = await _repo.GetActiveApartments();

            return Ok(apartments);
        }
        /* 

        [HttpGet("{/user/id}")]

        public async Task<IActionResult> GetActiveApartmentsFromUser(int id)
        { 
            var apartments = await _repo.GetActiveApartmentsFromUser(id);

            return Ok(apartments);
        }
        */
        
        [HttpGet("{id}")]

        public async Task<IActionResult> GetApartment(int id) 
        { 
            var apartment = await _repo.GetApartment(id);

            return Ok(apartment);
        }
    }
}