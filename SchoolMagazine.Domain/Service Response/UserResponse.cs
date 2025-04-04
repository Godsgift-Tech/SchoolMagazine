using SchoolMagazine.Domain.UserRoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Service_Response
{
    public class UserResponse
    {
        ////public string Token { get; set; } = null;
        ////public User User { get; set; } = null;
        //public bool Success { get; set; }  // Indicates success or failure
        //public string Token { get; set; } = null;
        //public User User { get; set; } = null;
        //public List<string> Errors { get; set; } = new List<string>(); // Store error messages


        public bool Success { get; set; }  // Indicates if the request was successful
        public string? Token { get; set; } // JWT Token
        public object? User { get; set; }  // User details (if needed)
        public string? Message { get; set; } // Message for success/failure
        public List<string>? Errors { get; set; } // List of errors if any
        public UserResponse()
        {
            Success = false; // Default to false
        }
    }
}
