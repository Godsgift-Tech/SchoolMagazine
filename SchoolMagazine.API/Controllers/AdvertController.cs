﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        [HttpGet("Get-AllPaid-Adverts")]
        public async Task<IActionResult> GetAllAdverts()
        {
            var result = await _advertService.GetAllAdvertsAsync();

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

       
        [HttpPost("post-and-pay-for adverts")]
        public async Task<IActionResult> PostAdvertAndPay([FromBody] AdvertPaymentRequestDto request)
        {
            if (request.Advert == null || request.PaymentDetails == null)
            {
                return BadRequest("Advert and payment details are required.");
            }

            var response = await _advertService.PostAdvertAndPayAsync(request.Advert, request.PaymentDetails);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }


    }
}
