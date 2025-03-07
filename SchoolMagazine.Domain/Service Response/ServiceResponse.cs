using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Service_Response
{
    
        public record ServiceResponse<T>(T Data, bool success = false, string message = null!);
    
}
