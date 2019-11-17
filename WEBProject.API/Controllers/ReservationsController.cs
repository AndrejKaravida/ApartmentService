using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WEBProject.API.Data;
using WEBProject.API.Dtos;
using WEBProject.API.Models;

namespace WEBProject.API.Controllers
{
    [Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationsForUser(int id)
        {
            var reservations = await _repo.GetReservationsForUser(id);

            var reservationsToReturn = _mapper.Map<IEnumerable<ReservationForReturnDto>>(reservations);

            return Ok(reservationsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody]JObject data)
        {

            int app_id = Int32.Parse(data["apartmentid"].ToString());

            Apartment apartmentFromRepo = _repo.GetApartmentSync(app_id);

            var startDate = data["startdate"].ToString();
            var endDate = data["enddate"].ToString();

            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            double numberOfNightsdouble = (end - start).TotalDays;
            numberOfNightsdouble = Math.Ceiling(numberOfNightsdouble);
            string numNights = numberOfNightsdouble.ToString();
            int numberOfNights = Int32.Parse(numNights);

            int totalPrice = numberOfNights * apartmentFromRepo.PricePerNight;

            int user_id = Int32.Parse(data["userid"].ToString());

            User userFromRepo = _repo.GetUserSync(user_id);

            Reservation newReservation = new Reservation()
            {
                Appartment = apartmentFromRepo,
                StartDate = start,
                EndDate = end,
                NumberOfNights = numberOfNights,
                TotalPrice = totalPrice,
                Guest = userFromRepo,
                Status = "Created"
            };

            _repo.Add(newReservation);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Saving review failed on save");
     
        }
     
    }
}