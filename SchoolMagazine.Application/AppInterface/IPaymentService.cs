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
        Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto paymentRequest);
        decimal CalculateExpectedAmount(DateTime startDate, DateTime endDate);
        bool ValidatePayment(decimal amountPaid, decimal expectedAmount);



    }
}
