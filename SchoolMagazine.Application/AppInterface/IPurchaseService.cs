using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IPurchaseService
    {
        Task<PurchaseProductResponseDto> CalculatePurchaseAsync(PurchaseProductRequestDto request);
    }
}
