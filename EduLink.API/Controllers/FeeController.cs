using EduLink.Core.IServices;
using EduLink.Utilities.DTO.Fee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : BaseController
    {
        private readonly IFeeService _feeService;

        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }
        [HttpGet("get-all-fees")]
        public async Task<IActionResult> GetAllFees()
        {
            var res = await _feeService.GetAllFeesAsync();
            return CreateResponse(res);
        }
        [HttpPost("add-fee")]
        public async Task<IActionResult> AddFee(AddFeeDTO dto)
        {
            var res = await _feeService.AddFeeAsync(dto);
            return CreateResponse(res);
        }
        [HttpGet("get-fee-by{id}")]
        public async Task<IActionResult>GetFeeByID(int id)
        {
            var res = await _feeService.GetFeeByIdAsync(id);
            return CreateResponse(res);
        }
        [HttpPost("mark-paid-fee{id}")]
        public async Task<IActionResult>MarkPaidFeed(int id)
        {
            var res = await _feeService.MarkFeeAsPaidAsync(id);
            return CreateResponse(res);
        }
        [HttpPut("update-fee")]
        public async Task<IActionResult>UpdateFee(int id,AddFeeDTO dto)
        {
            var res = await _feeService.UpdateFeeAsync(id, dto);
            return CreateResponse(res);
        }
        [HttpDelete("delete-fee{id}")]
        public async Task<IActionResult>DeleteFee(int id)
        {
            var res = await _feeService.DeleteFeeAsync(id);
            return CreateResponse(res);
        }
    }
}
