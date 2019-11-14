using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using WEBProject.API.Data;
using WEBProject.API.Dtos;
using WEBProject.API.Helpers;
using WEBProject.API.Models;
using System.Linq;

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
        [AllowAnonymous]
        public async Task<IActionResult> GetApartments([FromQuery]ApartmentParams apartmentParams)
        { 
            var apartments = await _repo.GetApartments(apartmentParams);

            var apartmentsToReturn = _mapper.Map<IEnumerable<ApartmentForListDto>>(apartments);

            Response.AddPagination(apartments.CurrentPage, apartments.PageSize, 
            apartments.TotalCount, apartments.TotalPages);

            return Ok(apartmentsToReturn);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetApartmentsForAdmin([FromQuery]ApartmentParams apartmentParams)
        {
            var apartments = await _repo.GetApartmentsForAdmin(apartmentParams);

            var apartmentsToReturn = _mapper.Map<IEnumerable<ApartmentForListDto>>(apartments);

            Response.AddPagination(apartments.CurrentPage, apartments.PageSize,
            apartments.TotalCount, apartments.TotalPages);

            return Ok(apartmentsToReturn);
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetApartmantsForUser(int userId, [FromQuery]ApartmentParams apartmentParams)
        {
            var apartments = await _repo.GetApartmentsFromUser(userId, apartmentParams);

            var apartmentsToReturn = _mapper.Map<IEnumerable<ApartmentForUserListDto>>(apartments);

            Response.AddPagination(apartments.CurrentPage, apartments.PageSize, 
            apartments.TotalCount, apartments.TotalPages);

            return Ok(apartmentsToReturn);
        }

        [HttpGet("{id}", Name="GetApartment")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApartment(int id) 
        { 
            var apartment = await _repo.GetApartment(id);

            var apartmentToReturn =  _mapper.Map<ApartmentForReturnDto>(apartment);

            return Ok(apartmentToReturn);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartment = await _repo.GetApartment(id);

            apartment.IsDeleted = true;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Deleting apartment failed on save");
        }

        [HttpGet("removeamentity/{ap_id}/{am_name}")]

        public async Task<IActionResult> RemoveAmentity(int ap_id, string am_name)
        {
            var apartment = await _repo.GetApartment(ap_id);

            var amentity = _repo.GetAmentity(am_name);

            apartment.Amentities.Remove(amentity);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Deleting amentity failed on save");
        }

        [HttpGet("changeprice/{ap_id}/{new_price}")]

        public async Task<IActionResult> ChangePrice (int ap_id, int new_price)
        {
            var apartment = await _repo.GetApartment(ap_id);

            if (new_price >= 0 && new_price <= 99)
            {
                apartment.PricePerNight = new_price;
                if (await _repo.SaveAll())
                    return NoContent();
                else
                    throw new Exception("Updating price failed on save");

            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("changearrival/{ap_id}/{new_time}")]
        public async Task<IActionResult> ChangeArrival (int ap_id, string new_time)
        {
            var apartment = await _repo.GetApartment(ap_id);

            apartment.TimeToArrive = new_time;
                
            if (await _repo.SaveAll())
                return NoContent();

            else
               throw new Exception("Updating price failed on save");

        }

        [HttpGet("changedeparture/{ap_id}/{new_time}")]
        public async Task<IActionResult> ChangeDeparture (int ap_id, string new_time)
        {
            var apartment = await _repo.GetApartment(ap_id);

            apartment.TimeToLeave = new_time;

            if (await _repo.SaveAll())
                return NoContent();

            else
                throw new Exception("Updating price failed on save");

        }

        [HttpGet("changeguests/{ap_id}/{new_number}")]

        public async Task<IActionResult> ChangeGuests(int ap_id, int new_number)
        {
            var apartment = await _repo.GetApartment(ap_id);

            if (new_number > 0 && new_number <= 10)
            {
                apartment.NumberOfGuests = new_number;
                if (await _repo.SaveAll())
                    return NoContent();
                else
                    throw new Exception("Updating guests failed on save");

            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("changerooms/{ap_id}/{new_number}")]

        public async Task<IActionResult> ChangeRooms(int ap_id, int new_number)
        {
            var apartment = await _repo.GetApartment(ap_id);

            if (new_number > 0 && new_number <= 10)
            {
                apartment.NumberOfRooms = new_number;
                if (await _repo.SaveAll())
                    return NoContent();
                else
                    throw new Exception("Updating rooms failed on save");

            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("makeactive/{ap_id}")]

        public async Task<IActionResult> MakeActive(int ap_id)
        {
            var apartment = await _repo.GetApartment(ap_id);

            apartment.Status = "Active";

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Updating rooms failed on save");
   
        }

        [HttpPost("addamentities/{ap_id}")]
        public async Task<IActionResult> AddAmenities(int ap_id, [FromBody]JObject data)
        {
            var apartment = await _repo.GetApartment(ap_id);

            string amenities = data["amenities"].ToString();

            string[] amentitiesParsed = amenities.Split(',');

            List<Amentity> amentities = new List<Amentity>();

            foreach (var str in amentitiesParsed)
            {
                Amentity am = new Amentity { Name = str };
                if (am.Name.Length > 0)
                    amentities.Add(am);
            }

            var amentitiesFromRepo = _repo.GetAmentities(amentities);

            apartment.Amentities = amentitiesFromRepo;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Updating amenities failed on save");
        }


        [HttpPost("{userId}")]
        public async Task<IActionResult> AddApartment(int userId, ApartmentForCreationDto apartmentForCreationDto)
        {
                 
                Apartment newapartment = new Apartment {Type = apartmentForCreationDto.Type, NumberOfRooms = apartmentForCreationDto.NumberOfRooms,
                NumberOfGuests = apartmentForCreationDto.NumberOfGuests, PricePerNight = apartmentForCreationDto.PricePerNight, 
                TimeToArrive = apartmentForCreationDto.TimeToArrive, TimeToLeave = apartmentForCreationDto.TimeToLeave, 
                Status = apartmentForCreationDto.Status, IsDeleted = false};

                if(apartmentForCreationDto.Apt.Length > 0) 
                {
                    apartmentForCreationDto.Street += ", apt." + apartmentForCreationDto.Apt;
                }

                var creatorFromRepo = _repo.GetUserSync(userId);
                newapartment.Host = creatorFromRepo;

                var addressFromRepo = _repo.GetAddress(apartmentForCreationDto.Street);

                if(addressFromRepo == null)
                {
                      Address address = new Address
                      {
                          Street = apartmentForCreationDto.Street,
                          City = apartmentForCreationDto.City,
                          Country = apartmentForCreationDto.Country,
                          ZipCode = apartmentForCreationDto.Zip
                      };
                      _repo.Add(address);
                       await _repo.SaveAll();
                       addressFromRepo = _repo.GetAddress(address.Street);
                }

                var locationFromRepo = _repo.GetLocation(addressFromRepo.Id);


                if(locationFromRepo == null){
                       Location location = new Location {Latitude = 0, Longitude = 0, Address = addressFromRepo};
                       _repo.Add(location);
                       await _repo.SaveAll();
                       locationFromRepo = _repo.GetLocation(addressFromRepo.Id);
                }

                newapartment.Location = locationFromRepo;

                string[] amentitiesParsed = apartmentForCreationDto.Amentities.Split(',');

                List<Amentity> amentities = new List<Amentity>();

                foreach(var str in amentitiesParsed)
                {
                    Amentity amentity = new Amentity {Name = str};
                    if(amentity.Name.Length > 0) 
                    amentities.Add(amentity);
                } 

                var amentitiesFromRepo = _repo.GetAmentities(amentities);

                newapartment.Amentities = amentitiesFromRepo;         
                
                _repo.Add(newapartment);

                if (await _repo.SaveAll())
                {
                    var apartmentToReturn = _mapper.Map<ApartmentForReturnDto>(newapartment);
                    return CreatedAtRoute("GetApartment", new { id = newapartment.Id }, apartmentToReturn);
                }

                throw new Exception("Creating the apartment failed on save.");
                            
        }
    }
}