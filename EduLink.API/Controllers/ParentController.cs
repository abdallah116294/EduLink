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
    }
}
