using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interface;
using api.Model;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenServices : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenServices(IConfiguration config)
        {
            _config=config;
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            
        }
        public string CreateToken(User user)
        {
            var claims=new List<Claim>{
               new Claim(ClaimTypes.Email,user.Email),
               new Claim(ClaimTypes.GivenName,user.UserName),
               new Claim(ClaimTypes.Role,user.Role)
              
            };
            var cred=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var discreptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
               Expires=DateTime.Now.AddDays(7),
               SigningCredentials=cred,
               Issuer=_config["JWT:Issuer"],
               Audience=_config["JWT:Audience"]
            };
            var tokenhandler=new JwtSecurityTokenHandler();
            var createToken=tokenhandler.CreateToken(discreptor);
            return tokenhandler.WriteToken(createToken);
      
        }

    }
}