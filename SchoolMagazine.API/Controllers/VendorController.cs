using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities.VendorEntities;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
      

        [HttpPost("register")]
        public async Task<IActionResult> RegisterVendor([FromBody] VendorDto vendorDto)
        {
            var response = await _vendorService.AddVendorAsync(vendorDto);

            if (!response.success)
                return BadRequest(response.message);

            return Ok(new { message = response.message, data = response.Data });
        }


        [HttpPost("{vendorId}/approve")]
        public async Task<IActionResult> ApproveVendor(Guid vendorId)
        {
            await _vendorService.ApproveVendorAsync(vendorId);
            return Ok("Vendor approved.");
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _vendorService.SubscribeVendorAsync(request);

            if (!result.success)
                return BadRequest(result.message);

            return Ok(result.message);
        }


        [HttpPost("{vendorId}/products")]
        public async Task<IActionResult> CreateProduct(Guid vendorId, [FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _vendorService.CreateProductAsync(vendorId, productDto);

            if (!result.success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateVendor([FromBody] VendorDto vendor)
        {
            if (vendor == null || vendor.Id == Guid.Empty)
                return BadRequest(new { success = false, message = "Invalid vendor data." });

            try
            {
                var result = await _vendorService.UpdateVendorAsync(vendor);

                if (!result.success)
                    return BadRequest(new { success = false, message = result.message });

                return Ok(new
                {
                    success = true,
                    message = result.message,
                    data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        //[HttpPut("{vendorId}/products/{productId}")]
        //public async Task<IActionResult> UpdateProduct(Guid vendorId, Guid productId, [FromBody] UpdateProductDto productDto)
        //{
        //    if (productDto == null || productId != productDto.Id)
        //        return BadRequest(new { success = false, message = "Product ID mismatch or invalid request." });

        //    var response = await _vendorService.UpdateProductAsync(vendorId, productDto);

        //    if (!response.success)
        //        return BadRequest(new { success = false, message = response.message });

        //    return Ok(new
        //    {
        //        success = true,
        //        message = response.message,
        //        data = response.Data
        //    });
        //}

        [HttpPut("{vendorId}/update-product/{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid vendorId, Guid productId, [FromBody] UpdateProductDto productDto)
        {
            var result = await _vendorService.UpdateProductAsync(vendorId, productId, productDto);

            if (!result.success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("products/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _vendorService.DeleteProductAsync(productId);
            return Ok("Product deleted.");
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetAllApprovedVendors([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _vendorService.GetAllApprovedVendorsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        

    }
}
