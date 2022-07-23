using IfIFitz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IfIFitz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeRepository _sizeRepo;

        public SizeController(ISizeRepository sizeRepo)
        {
            _sizeRepo = sizeRepo;
        }

        [HttpGet]
        public IActionResult GetSizes()
        {
            return Ok(_sizeRepo.GetAllSizes());
        }
    }
}
