using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using IfIFitz.Models;
using IfIFitz.Repositories;

namespace IfIFitz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetUserProfile(string firebaseUserId)
        {
            return Ok(_userProfileRepository.GetByFirebaseUserId(firebaseUserId));
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_userProfileRepository.GetAllUsers());
        }

        [HttpGet("Details/{id}")]
        public IActionResult GetUserProfileById(int id)
        {
            return Ok(_userProfileRepository.GetByUserId(id));
        }

        //[HttpGet("LoggedInUserCheck")]
        //public IActionResult CurrentUserCheck(string firebaseUserId)
        //{
        //    var currentUserProfile = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        //    UserProfile userProfile = GetCurrentUserProfile();
        //    if (currentUserProfie.Id == userProfile.Id)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        throw new Exception("User not a match");
        //    }
        //}

        [HttpPost]
        public IActionResult Post(UserProfile userProfile)
        {
            userProfile.IsActive = true;
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction(
                nameof(GetUserProfile),
                new { firebaseUserId = userProfile.FirebaseUserId },
                userProfile);
        }
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}