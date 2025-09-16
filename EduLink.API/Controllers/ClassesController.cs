using EduLink.Core.IServices;
using EduLink.Utilities.DTO.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : BaseController
    {
        private readonly IClassService _classService;
        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }
        [HttpPost("create-class")]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDTO createClassDTO)
        {
            var result = await _classService.CreateClassAsync(createClassDTO);
            if (result.IsSuccess)
            {
                return CreateResponse(result);
            }
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var result = await _classService.GetClassByIdAsync(id);
            if (result.IsSuccess)
            {
                return CreateResponse(result);
            }
            return CreateResponse(result);
        }
        [HttpGet("all-classes")]
        public async Task<IActionResult> GetAllClasses()
        {
            var result = await _classService.GetAllClassesAsync();
            if (result.IsSuccess)
            {
                return CreateResponse(result);
            }
            return CreateResponse(result);
        }
        [HttpPut("update-class/{id}")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody] UpdateClassDTO updateClassDTO)
        {
            var result = await _classService.UpdateClassAsync(id, updateClassDTO);
            if (result.IsSuccess)
            {
                return CreateResponse(result);
            }
            return CreateResponse(result);
        }
        [HttpDelete("delete-class/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var result = await _classService.DeleteClassAsync(id);
            if (result.IsSuccess)
            {
                return CreateResponse(result);
            }
            return CreateResponse(result);
        }
    }
}
