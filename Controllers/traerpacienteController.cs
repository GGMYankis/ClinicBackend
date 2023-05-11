using Clinica.Modelos;
using Clinica.SqlTblas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

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

        //[HttpPost]
        //[Route("CrearEvaluacion")]
        //public IActionResult CrearEvaluacion([FromBody] TdTablas.Evaluation obj)
        //{
        //    int idEvaluacion = 0;
        //    try
        //    {
        //        using (var conexion = new SqlConnection(cadenaSQL))
        //        {
        //            var cmd = new SqlCommand("sp_EvaluacionProbar", conexion);
        //            cmd.Parameters.AddWithValue("IdPatients", obj.IdPatients);
        //            cmd.Parameters.AddWithValue("IdTherapy", obj.IdTherapy);
        //            cmd.Parameters.AddWithValue("Price", obj.Price);
        //            cmd.Parameters.AddWithValue("IdTerapeuta", obj.IdTerapeuta);
        //            cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            conexion.Open();
        //            cmd.ExecuteNonQuery();
        //            //idEvaluacion = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
        //            //var cmdRe = new SqlCommand("sp_recurrencia", conexion);
        //            //cmdRe.Parameters.AddWithValue("FechaInicio", obj.FechaInicio);
        //            //cmdRe.Parameters.AddWithValue("Repetir", obj.Repetir);
        //            //cmdRe.Parameters.AddWithValue("Frecuencia", obj.Frecuencia);
        //            //cmdRe.Parameters.AddWithValue("Dias", obj.Dias);
        //            //cmdRe.Parameters.AddWithValue("IdEvaluation", idEvaluacion);
        //            //cmdRe.CommandType = CommandType.StoredProcedure;
        //            //cmdRe.ExecuteNonQuery();
        //        }
        //        return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
        //    }
        //}

        //-------------------------------->

        [HttpPost]
        [Route("CrearEvaluacion")]
        public IActionResult CrearEvaluacion([FromBody] Evaluation objeto)
        {
            try
            {
                _dbcontext.Evaluations.Add(objeto);
                _dbcontext.SaveChanges();

                var idObtenido =  CreatedAtAction(nameof(ObtenerUsuario), new { id = objeto.Id }, objeto);

                var reId = objeto.Id;

                return Ok(reId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
       
         }


        [HttpPost]
        [Route("CrearRecurrencia")]
        public IActionResult CrearRecurrencia([FromBody] Recurrencium recurrencia)
        {
            try
            {
                _dbcontext.Recurrencia.Add(recurrencia);
                _dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> ObtenerUsuario(int id)
        {
            var resId = await _dbcontext.Evaluations.FindAsync(id);
            if (resId == null)
            {
                return NotFound();
            }
            return resId;
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

