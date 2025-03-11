using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Service_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IPaymentService
    {
        //Task<AdvertServiceResponse<string>> ProcessPayment(decimal amount, string currency, string paymentMethod);
       Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto paymentRequest);
        
        
    // Task<PaymentResponseDto> ProcessPaymentAsync(decimal amount, string currency, string paymentMethod);
        

    }
}
