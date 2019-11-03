using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WEBProject.API.Data;
using WEBProject.API.Dtos;
using WEBProject.API.Helpers;
using WEBProject.API.Models;

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

        public async Task<IActionResult> GetActiveApartments([FromQuery]ApartmentParams apartmentParams)
        { 
            var apartments = await _repo.GetActiveApartments(apartmentParams);

            var apartmentsToReturn = _mapper.Map<IEnumerable<ApartmentForListDto>>(apartments);

            Response.AddPagination(apartments.CurrentPage, apartments.PageSize, 
            apartments.TotalCount, apartments.TotalPages);

            return Ok(apartmentsToReturn);
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetApartmantsForUser(int userId, [FromQuery]ApartmentParams apartmentParams)
        {
            var apartments = await _repo.GetApartmentsFromUser(userId, apartmentParams);

            var apartmentsToReturn = _mapper.Map<IEnumerable<ApartmentForListDto>>(apartments);

            Response.AddPagination(apartments.CurrentPage, apartments.PageSize, 
            apartments.TotalCount, apartments.TotalPages);

            return Ok(apartmentsToReturn);
        }

        [HttpGet("{id}", Name="GetApartment")]

        public async Task<IActionResult> GetApartment(int id) 
        { 
            var apartment = await _repo.GetApartment(id);

            var apartmentToReturn =  _mapper.Map<ApartmentForReturnDto>(apartment);

            return Ok(apartmentToReturn);
        }
      
  
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddApartment(int userId, ApartmentForCreationDto apartmentForCreationDto)
        {
                 
                Apartment newapartment = new Apartment {Type = apartmentForCreationDto.Type, NumberOfRooms = apartmentForCreationDto.NumberOfRooms,
                NumberOfGuests = apartmentForCreationDto.NumberOfGuests, PricePerNight = apartmentForCreationDto.PricePerNight, 
                TimeToArrive = apartmentForCreationDto.TimeToArrive, TimeToLeave = apartmentForCreationDto.TimeToLeave, 
                Status = apartmentForCreationDto.Status};

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