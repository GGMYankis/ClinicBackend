using Clinica.TdTablas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Data;
using System.Data.SqlClient;



namespace Clinica.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class traerpacienteController : ControllerBase
    {
        public readonly dbapiContext _dbcontext;

        private readonly string cadenaSQL;
        public traerpacienteController(IConfiguration config, dbapiContext _context)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
            _dbcontext = _context;
        }

        [HttpPost]
        [Route("CrearEvaluacion")]
        public IActionResult CrearEvaluacion([FromBody] TdTablas.Evaluation obj)
        {

            int idEvaluacion = 0;
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {

                    var cmd = new SqlCommand("sp_EvaluacionProbar", conexion);
                    cmd.Parameters.AddWithValue("IdPatients", obj.IdPatients);
                    cmd.Parameters.AddWithValue("IdTherapy", obj.IdTherapy);
                    cmd.Parameters.AddWithValue("Price", obj.Price);
                    cmd.Parameters.AddWithValue("IdTerapeuta", obj.IdTerapeuta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    idEvaluacion = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                    var cmdRe = new SqlCommand("sp_recurrencia", conexion);
                    cmdRe.Parameters.AddWithValue("FechaInicio", obj.FechaInicio);
                    cmdRe.Parameters.AddWithValue("Repetir", obj.Repetir);
                    cmdRe.Parameters.AddWithValue("Frecuencia", obj.Frecuencia);
                    cmdRe.Parameters.AddWithValue("Dias", obj.Dias);
                    cmdRe.Parameters.AddWithValue("IdEvaluation", idEvaluacion);
                    cmdRe.CommandType = CommandType.StoredProcedure;


                    cmdRe.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }

        }




     
        public static IActionResult ddxxxLista()
        {
         
        }

        
    }
}

