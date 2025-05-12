using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseProductService;

        public PurchaseController(IPurchaseService purchaseProductService)
        {
            _purchaseProductService = purchaseProductService;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculatePurchase([FromBody] PurchaseProductRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _purchaseProductService.CalculatePurchaseAsync(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
