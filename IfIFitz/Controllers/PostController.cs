using IfIFitz.Models;
using IfIFitz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace IfIFitz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        private readonly IUserProfileRepository _userProfileRepo;

        public PostController(IPostRepository postRepo, IUserProfileRepository userProfileRepo)
        {
            _postRepo = postRepo;
            _userProfileRepo = userProfileRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_postRepo.GetAllPosts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _postRepo.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public IActionResult Post(Post post)
        {
            UserProfile currentUser = GetCurrentUserProfile();
            post.UserProfileId = currentUser.Id;
            post.CreatedDateTime = DateTime.Now;
            _postRepo.AddPost(post);
            return Ok(post);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _postRepo.UpdatePost(post);
            return NoContent();
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepo.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
