using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WEBProject.API.Data;
using WEBProject.API.Models;

namespace WEBProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IApartmentBookRepository _repo;
        private readonly IMapper _mapper;
        public CommentsController(IApartmentBookRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("{ap_id}")]
        public async Task<IActionResult> AddReview(int ap_id, [FromBody]JObject data)
        {
            string content = data["content"].ToString();
            int grade = Int32.Parse(data["grade"].ToString());
            int userId = Int32.Parse(data["userid"].ToString());

            var apartment = await _repo.GetApartment(ap_id);
            var user = await _repo.GetUser(userId);

            Comment comment = new Comment { Apartment = apartment, User = user, Text = content, Grade = grade, Deleted = false, Approved = false };

            apartment.Comments.Add(comment);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Saving review failed on save");
        }

        [HttpGet("approve/{id}")]
        public async Task<IActionResult> ApproveComment(int id)
        {
            var comment = await _repo.GetComment(id);

            comment.Approved = true;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Approving comment failed on save");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _repo.GetComment(id);

            comment.Deleted = true;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Deleting comment failed on save");
        }

        [HttpGet("getpermission/{us_id}/{app_id}")]
        public async Task<IActionResult> GetCommentPermission(int us_id, int app_id)
        {
            if (_repo.GetReservationsForUserForApartment(us_id, app_id))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}