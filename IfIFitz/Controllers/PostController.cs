using IfIFitz.Models;
using IfIFitz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
            var currentUser = GetCurrentUserProfile();
            
            if (post.UserProfileId != currentUser.Id)
            {
                return BadRequest();
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            _postRepo.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var currentPost = _postRepo.GetPostById(id);
            var currentUser = GetCurrentUserProfile();
            if (currentPost.UserProfileId != currentUser.Id)
            {
                return BadRequest();
            }

            _postRepo.DeletePost(id);
            return NoContent();
        }

        [HttpGet("User")]
        public IActionResult GetCurrentUsersPosts()
        {
            UserProfile currentUser = GetCurrentUserProfile();
            return Ok(_postRepo.GetPostsByUserId(currentUser.Id));
        }

        [HttpGet("User/{id}")]
        public IActionResult GetPostsByUserId(int id)
        {
            return Ok(_postRepo.GetPostsByUserId(id));
        }

        [HttpGet("Favorite")]
        public IActionResult GetCurrentUsersFavoritedPosts()
        {
            UserProfile currentUser = GetCurrentUserProfile();
            return Ok(_postRepo.GetUsersFavoritedPosts(currentUser.Id));
        }

        [HttpGet("Favorite/{id}")]
        public IActionResult GetFavoritedPostsByUserId(int id)
        {
            return Ok(_postRepo.GetUsersFavoritedPosts(id));
        }

        [HttpPost("Favorite/{id}")]
        public IActionResult PostFavorite(int id)
        {
            var currentUser = GetCurrentUserProfile();
            var currentPost = _postRepo.GetPostById(id);
            
            if (currentUser.Id == currentPost.UserProfileId)
            {
                return BadRequest();
            }

            try
            {
                _postRepo.AddFavorite(currentUser.Id, id);
                return NoContent();
            }
            catch(SqlException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("Favorite/{id}")]
        public IActionResult DeleteFavorite(int id)
        {
            var currentUser = GetCurrentUserProfile();

            try
            {
                _postRepo.DeleteFavorite(currentUser.Id, id);
                return NoContent();
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepo.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
