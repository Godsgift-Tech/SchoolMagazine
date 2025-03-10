using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Service_Response
{
    public class EventServiceResponse<T>
    {
        public T Data { get; set; }   // Holds the actual response data
        public bool Success { get; set; } = true;   // Default to success
        public string Message { get; set; } = string.Empty;   // Holds error/success message
    }
}
