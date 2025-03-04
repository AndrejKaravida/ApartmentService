﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("apt/{id}")]
        public async Task<IActionResult> GetReservationsForApartment(int id)
        {
            var reservations = await _repo.GetReservationsForApartment(id);

            var reservationsToReturn = _mapper.Map<IEnumerable<ApartmanReservationsDto>>(reservations);

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

            double totalPrice = numberOfNights * apartmentFromRepo.PricePerNight;

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

            int numOfWeekendDays = 0;
            int numOfHolidayDays = 0;

            if ( apartmentFromRepo.ReservedDates.Count == 0)
            {
                for (var dt = start; dt <= end; dt = dt.AddDays(1))
                {
                   ReservedDate date = new ReservedDate { Date = dt, CurrentNumberOfGuests = 1 };
                   apartmentFromRepo.ReservedDates.Add(date);
                    if (Helpers.Holidays.holidays.Contains(dt))
                    {
                        numOfHolidayDays++;
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Friday || dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        numOfWeekendDays++;
                    }
                    if (apartmentFromRepo.NumberOfGuests == date.CurrentNumberOfGuests)
                    {
                        BlockedDate bd = new BlockedDate { Date = date.Date };
                        apartmentFromRepo.BlockedDates.Add(bd);
                    }
                }   
                if (numOfWeekendDays > 0)
                {
                    var discount = apartmentFromRepo.PricePerNight * 0.1 * numOfWeekendDays;
                    newReservation.TotalPrice -= discount;
                }
                if (numOfHolidayDays > 0)
                {
                    var priceIncrease = apartmentFromRepo.PricePerNight * 0.05 * numOfHolidayDays;
                    newReservation.TotalPrice += priceIncrease;
                }
            }
            else
            {
                List<ReservedDate> reservedDates = new List<ReservedDate>();

                for (var dt = start; dt <= end; dt = dt.AddDays(1))
                {
                    ReservedDate date = new ReservedDate { Date = dt };
                    reservedDates.Add(date);
                    if (Helpers.Holidays.holidays.Contains(dt))
                    {
                        numOfHolidayDays++;
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Friday || dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        numOfWeekendDays++;
                    }    
                }

               foreach (var rd in reservedDates)
               {
                  var dateToChange = apartmentFromRepo.ReservedDates.FirstOrDefault(date => date.Date == rd.Date) ?? null;

                    if(dateToChange != null)
                    {
                        dateToChange.CurrentNumberOfGuests++;
                        if (apartmentFromRepo.NumberOfGuests == dateToChange.CurrentNumberOfGuests)
                        {
                            BlockedDate bd = new BlockedDate { Date = dateToChange.Date };
                            apartmentFromRepo.BlockedDates.Add(bd);
                        }
                    }
                    else
                    {
                        ReservedDate date = new ReservedDate { Date = rd.Date, CurrentNumberOfGuests = 1 };
                        apartmentFromRepo.ReservedDates.Add(date);

                        if (apartmentFromRepo.NumberOfGuests == date.CurrentNumberOfGuests)
                        {
                            BlockedDate bd = new BlockedDate { Date = date.Date };
                            apartmentFromRepo.BlockedDates.Add(bd);
                        }
                    }              
               }

                if (numOfWeekendDays > 0)
                {
                    var discount = apartmentFromRepo.PricePerNight * 0.1 * numOfWeekendDays;
                    newReservation.TotalPrice -= discount;
                }
                if (numOfHolidayDays > 0)
                {
                    var priceIncrease = apartmentFromRepo.PricePerNight * 0.05 * numOfHolidayDays;
                    newReservation.TotalPrice += priceIncrease;
                }
            }
             
              _repo.Add(newReservation);
               apartmentFromRepo.Reservations.Add(newReservation);

            if (await _repo.SaveAll())
            return NoContent();

            throw new Exception("Saving review failed on save");
     
        }

        [HttpGet("accept/{id}")]
        public async Task<IActionResult> AcceptReservation(int id)
        {
            var reservation = await _repo.GetReservation(id);

            reservation.Status = "Accepted";

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Accepting reservation failed on save");
        }

        [HttpGet("deny/{id}")]
        public async Task<IActionResult> DenyReservation(int id)
        {
            var reservation = await _repo.GetReservation(id);

            reservation.Status = "Denied";

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Deniding reservation failed on save");
        }

        [HttpGet("finish/{id}")]
        public async Task<IActionResult> FinishReservation(int id)
        {
            var reservation = await _repo.GetReservation(id);

            if(reservation.EndDate <= DateTime.Now)
            {
                reservation.Status = "Finished";
                if (await _repo.SaveAll())
                    return NoContent();
            }
            else
            {
                return Unauthorized();
            }

            throw new Exception("Finishing reservation failed on save");
        }

        [HttpGet("quit/{id}")]
        public async Task<IActionResult> QuitReservation(int id)
        {
            var reservation = await _repo.GetReservation(id);

            reservation.Status = "Quit";

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Quiting reservation failed on save");
        }

    }
}