﻿using IfIFitz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IfIFitz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IUserProfileRepository _userProfileRepo;

        public CommentController(ICommentRepository commentRepo, IUserProfileRepository userProfileRepo)
        {
            _commentRepo = commentRepo;
            _userProfileRepo = userProfileRepo;
        }

        [HttpGet("Post/{id}")]
        public IActionResult GetByPostId(int id)
        {
            return Ok(_commentRepo.GetCommentsByPostId(id));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var comment = _commentRepo.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
    }
}