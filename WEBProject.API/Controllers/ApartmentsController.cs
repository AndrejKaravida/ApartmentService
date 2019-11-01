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

        public async Task<IActionResult> GetActiveApartments()
        { 
            var apartments = await _repo.GetActiveApartments();

            var apartmentsToReturn = _mapper.Map<IEnumerable<ApartmentForListDto>>(apartments);

            return Ok(apartmentsToReturn);
        }
             
        [HttpGet("{id}", Name="GetApartment")]

        public async Task<IActionResult> GetApartment(int id) 
        { 
            var apartment = await _repo.GetApartment(id);

            return Ok(apartment);
        }
      
  
        [HttpPost("{id}")]
        public async Task<IActionResult> AddApartment(int userId, ApartmentForCreationDto apartmentForCreationDto)
        {
                var creator = await _repo.GetUser(userId);
                 
                Apartment newapartment = new Apartment {Type = apartmentForCreationDto.Type, NumberOfRooms = apartmentForCreationDto.NumberOfRooms,
                NumberOfGuests = apartmentForCreationDto.NumberOfGuests, PricePerNight = apartmentForCreationDto.PricePerNight, 
                TimeToArrive = apartmentForCreationDto.TimeToArrive, TimeToLeave = apartmentForCreationDto.TimeToLeave, 
                Status = apartmentForCreationDto.Status};

                if(apartmentForCreationDto.Apt.Length > 0) 
                {
                    apartmentForCreationDto.Street += ", apt." + apartmentForCreationDto.Apt;
                }

                Address address = new Address {Street = apartmentForCreationDto.Street,
                 City = apartmentForCreationDto.City, ZipCode = apartmentForCreationDto.Zip};

                var addressFromRepo = _repo.GetAddress(address.Street);

                if(addressFromRepo.Street.Length == 0){
                 _repo.Add(address);
                 await _repo.SaveAll();
                }

                Location location = new Location {Latitude = 0, Longitude = 0, Address = addressFromRepo};

                var locationFromRepo = _repo.GetLocation(address);

                 if(locationFromRepo.Address.Street.Length == 0){
                 _repo.Add(location);
                 await _repo.SaveAll();
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
                /* 
                _repo.Add(newapartment);


                if (await _repo.SaveAll())
                {
                    var apartmentToReturn = _mapper.Map<ApartmentForReturnDto>(newapartment);
                    return CreatedAtRoute("GetApartment", new { id = newapartment.Id }, apartmentToReturn);
                }

                throw new Exception("Creating the apartment failed on save.");
                */
            

            return Ok(newapartment);
        }
    }
}