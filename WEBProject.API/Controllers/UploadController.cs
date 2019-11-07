using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBProject.API.Data;
using WEBProject.API.Models;

namespace WEBProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private readonly IImageHandler _imageHandler;
        private readonly IApartmentBookRepository _repo;
        private readonly IMapper _mapper;

        public UploadController(IImageHandler imageHandler, IApartmentBookRepository repo, IMapper mapper)
        {
            _imageHandler = imageHandler;
            _repo = repo;
            _mapper = mapper;
        }


        [HttpPost("{apartmentId}")]
        public async Task<IActionResult> UploadImage(int apartmentId, IFormFile file)
        {
            var apartment = _repo.GetApartmentSync(apartmentId);
            var image_location = await _imageHandler.UploadImage(file); 
            var objectResult = image_location as ObjectResult;
            var value = objectResult.Value;
            
            Photo newPhoto = new Photo() { Apartment = apartment, Url = "http://localhost:5000/" + value.ToString(), Description = "Main Photo" };

            _repo.Add(newPhoto);
            _repo.SaveAll();

            apartment.Photos.Add(newPhoto);

            _repo.SaveAll();

            return Ok();
        }

    }
}