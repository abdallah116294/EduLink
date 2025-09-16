using EduLink.Core.IServices;
using EduLink.Utilities.DTO.Parent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : BaseController
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }
        [Authorize(Roles = "PARENT")]
        [HttpPost("add-parent")]
        public async Task<IActionResult>AddParent(CreateParentDTO dto)
        {
            dto.UserId = GetUserId();
            var res = await _parentService.CreateParent(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-all-Parents")]
        public async Task<IActionResult> GetAllParents()
        {
            var res = await _parentService.GetAllParent();
            return CreateResponse(res);
        }
        [Authorize(Roles ="PARENT")]
        [HttpGet("GetParent")]
        public async Task<IActionResult> GetParent()
        {
            var userId = GetUserId();
            var res = await _parentService.GetParent(userId);
            return CreateResponse(res);
        }
        [Authorize(Roles = "PARENT")]
        [HttpGet("get-students")]
        public async Task<IActionResult> GetParentStudent()
        {
            var userId = GetUserId();
            var res = await _parentService.GetStudentByParentId(userId);
            return CreateResponse(res);
        }
        [HttpDelete("delete-parent/{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var res = await _parentService.DeleteParent(id);
            return CreateResponse(res);
        }
    }
}
