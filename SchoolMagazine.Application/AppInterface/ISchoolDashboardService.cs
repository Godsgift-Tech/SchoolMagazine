using SchoolMagazine.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppInterface
{
    public interface ISchoolDashboardService
    {
        Task<SchoolDashboardDto> GetDashboardDataAsync(Guid schoolId, DateTime? fromDate = null, DateTime? toDate = null);
    }
}
