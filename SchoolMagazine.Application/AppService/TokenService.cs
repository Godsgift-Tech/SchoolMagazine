//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using SchoolMagazine.Application.AppInterface;
//using SchoolMagazine.Domain.UserRoleInfo;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace SchoolMagazine.Application.AppService
//{


//    public class TokenService : ITokenService
//    {
//        private readonly string _secret;
//        private readonly string _issuer;
//        private readonly string _audience;
//        private readonly int _expirationMinutes;

//        public TokenService(IConfiguration configuration)
//        {
//            _secret = configuration["JwtSettings:Secret"];
//            _issuer = configuration["JwtSettings:Issuer"] ?? "defaultIssuer";
//            _audience = configuration["JwtSettings:Audience"] ?? "defaultAudience";
//            _expirationMinutes = int.TryParse(configuration["JwtSettings:Expiration"], out var exp) ? exp : 60;

//            if (string.IsNullOrEmpty(_secret))
//            {
//                throw new ArgumentNullException("JwtSettings:Secret", "JWT Secret cannot be null. Check appsettings.json or environment variables.");
//            }
//        }

//        public string GenerateToken(User user, IList<string> roles)
//        {
//            if (user == null)
//                throw new ArgumentNullException(nameof(user), "User cannot be null.");

//            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
//                throw new ArgumentException("User properties cannot be null or empty.");

//            if (roles == null || roles.Count == 0)
//                throw new ArgumentException("User roles cannot be null or empty.");

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new List<Claim>
//        {
//            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
//            new Claim(JwtRegisteredClaimNames.Email, user.Email),
//            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
//        };

//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            var token = new JwtSecurityToken(
//                issuer: _issuer,
//                audience: _audience,
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }


//}
