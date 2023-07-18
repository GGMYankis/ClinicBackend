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
using Clinica.ModelEntity;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Clinica.Modelos;
using System.Globalization;

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
                var user = _dbcontext.Users.FirstOrDefault(u => u.Names == email);
                return StatusCode(StatusCodes.Status200OK, new {  user }); ;
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
            
            var message = "Usuario no encontardo";
            User user = _dbcontext.Users.FirstOrDefault(u => u.Email == usuario.Email && u.Password == usuario.Password);

            if(user == null) {

                return BadRequest(message);

            }

                var tokencreado = Token(user);

                return StatusCode(StatusCodes.Status200OK, new { tokencreado, user });      
        }



        private string Token(ModelEntity.User user)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Names));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string tokencreado = tokenHandler.WriteToken(tokenConfig);
            return tokencreado;
        }


        [HttpPost]
        [Route("GastosGanancia")]
        public List<Buscar> GastosGanancia([FromBody] Buscar obj)
        {
            List<Buscar> lista = new List<Buscar>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {

                    SqlCommand cmd = new SqlCommand("sp_buscar", conexion);
                    cmd.Parameters.AddWithValue("@fechainicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFinal", obj.FechaFinal);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Buscar()
                                {

                                    Price = Convert.ToInt32(dr["Price"], new CultureInfo("es-PE")),
                                    Label = dr["Label"].ToString(),
                                    FechaInicio = Convert.ToDateTime(dr["FechaInicio"])

                                }
                                );
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                lista = new List<Buscar>();
            }
            return lista;
        }


}
}

