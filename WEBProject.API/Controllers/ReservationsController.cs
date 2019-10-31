using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBProject.API.Data;
using WEBProject.API.Dtos;

namespace WEBProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IApartmentBookRepository _repo;
        private readonly IMapper _mapper;
        public ReservationsController(IApartmentBookRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _repo.GetReservations();

            var reservationsToReturn = _mapper.Map<IEnumerable<ReservationForReturnDto>>(reservations);

            return Ok(reservationsToReturn);
        }
                
    }
}