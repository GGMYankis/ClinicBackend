using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using Nest;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Clinica.TdTablas;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;
        private readonly string cadenaSQL;
        public readonly dbapiContext _dbcontext;
        public AutenticacionController(IConfiguration config, dbapiContext dbcontext)
        {
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
            cadenaSQL = config.GetConnectionString("CadenaSQL");
            _dbcontext = dbcontext;
        }

        [Authorize]
        [HttpPost]
        [Route("getUserByToken")]
        public IActionResult getUserByToken()
        {
            try
            {
                var email = User.Claims.ElementAt(1).Value;
                var user = _dbcontext.Users.FirstOrDefault(u => u.Email == email);
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(User usuario)
        {
            User user = new User();
            var message = "Usuario no encontardo";
            user = _dbcontext.Users.FirstOrDefault(u => u.Email == usuario.Email && u.Password == usuario.Password);
            if(user == null) {
                return StatusCode(StatusCodes.Status200OK, new { message, user }); ;
            }
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Names));
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string tokencreado = tokenHandler.WriteToken(tokenConfig);
                return StatusCode(StatusCodes.Status200OK, new { tokencreado, user });      
        }
    }
}

