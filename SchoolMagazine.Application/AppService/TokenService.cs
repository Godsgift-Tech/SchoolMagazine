using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Domain.UserRoleInfo;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolMagazine.Application.AppService
{


    
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;    // this  enables us to use apsettings.json

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

       
        public string CreateJWTToken(User user, List<string> roles)
        {
            // Create claim
            var claim = new List<Claim>();
            claim.Add(new Claim (ClaimTypes.Email, user.Email));
            //adding roles to claim
            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));

            }
            // working on token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  //from appsettings.json
            var credentials =new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
               _config["Jwt: Issuer"],
               _config["jWT: Audience"],
               claim,
               expires: DateTime.Now.AddMinutes(15),
               signingCredentials: credentials);
            // return token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }


}
