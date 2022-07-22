using IfIFitz.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IfIFitz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialRepository _materialRepo;

        public MaterialController(IMaterialRepository materialRepo)
        {
            _materialRepo = materialRepo;
        }

        [HttpGet]
        public IActionResult GetMaterials()
        {
            return Ok(_materialRepo.GetAllMaterials());
        }
    }
}
