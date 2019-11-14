using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBProject.API.Data;
using WEBProject.API.Models;

namespace WEBProject.API.Controllers
{
    [Authorize]
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

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            return Ok(photoFromRepo);
        }

        [HttpGet("setmain/{appId}/{id}")]
        public async Task<IActionResult> SetMain(int appId, int id)
        {
            var photo = await _repo.GetPhoto(id);

            photo.IsMain = true;

            var previousMain = await _repo.GetMainPhotoForApartment(appId);

            previousMain.IsMain = false;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Setting main photo failed on save");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _repo.GetPhoto(id);

            photo.IsDeleted = true;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Deleting photo failed on save");
        }


        [HttpPost("{apartmentId}")]
        public async Task<IActionResult> UploadImage(int apartmentId, IFormFile file)
        {
            var apartment = _repo.GetApartmentSync(apartmentId);
            var image_location = await _imageHandler.UploadImage(file); 
            var objectResult = image_location as ObjectResult;
            var value = objectResult.Value;
                        
            Photo newPhoto = new Photo() { Apartment = apartment, Url = "http://localhost:5000/" + value.ToString(), IsDeleted = false };

            if(!apartment.Photos.Any(p => p.IsMain))
            {
                newPhoto.IsMain = true;
            }
            else
            {
                newPhoto.IsMain = false;
            }

            _repo.Add(newPhoto);
            
            apartment.Photos.Add(newPhoto);

            if (await _repo.SaveAll())
            {
                return CreatedAtRoute("GetPhoto", new { id = newPhoto.Id }, newPhoto);
            }

            return BadRequest("Could not add the photo");

        }

        

    }
}