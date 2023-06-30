using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clinica.SqlTables;
using Clinica.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {

        public readonly dbapiContext _dbcontext;

        public CitasController(dbapiContext _context, IConfiguration config, ILogger<ClinicaController> logger)
        {
            _dbcontext = _context;

        }


        [HttpGet]
        [Route("Citas")]
        public async Task<object> Citas()
        {

            string mensaje = string.Empty;
            List<Evaluation> viewModal = new List<Evaluation>();
            List<UserEvaluacion> olista = new List<UserEvaluacion>();
            List<Buscar> recu = new List<Buscar>();
            List<Buscar> InfoProcesada = new List<Buscar>();

            try
            {


                using (var dbContext = _dbcontext)
                {
                    var result = from r in dbContext.Recurrencia

                                 select new Buscar
                                 {
                                     FechaInicio = r.FechaInicio,
                                     Repetir = r.Repetir,
                                     Frecuencia = r.Frecuencia,
                                     Dias = r.Dias,
                                     IdEvaluation = r.IdEvaluation,
                                     IdRecurrencia = r.IdRecurrencia
                                 };

                    recu = await result.ToListAsync();

                    foreach (var pro in recu)
                    {
                        var idEva = pro.IdEvaluation;
                        Buscar recuProcesada = InfoProcesada.FirstOrDefault(f => f.IdEvaluation == idEva);

                        if (recuProcesada == null)
                        {
                            InfoProcesada.Add(pro); // Agregar 'pro' en lugar de 'recuProcesada'
                        }
                    }


                    foreach (var listado in InfoProcesada)
                    {

                        var idEva = listado.IdEvaluation;


                        var resultEva = from e in dbContext.Evaluations
                                        join c in dbContext.Consultorios on e.IdConsultorio equals c.IdConsultorio
                                        join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                        join p in dbContext.Patients on e.IdPatients equals p.IdPatients
                                        join u in dbContext.Users on e.IdTerapeuta equals u.IdUser
                                        where e.Id == idEva && p.Activo == true
                                        select new Modelos.UserEvaluacion
                                        {
                                            IdEvaluacion = idEva,
                                            Terapeuta = new User
                                            {
                                                IdUser = u.IdUser,
                                                Names = u.Names,
                                                Apellido = u.Apellido
                                            },
                                            Terapia = new Therapy
                                            {
                                                IdTherapy = t.IdTherapy,
                                                Label = t.Label
                                            },

                                            Paciente = new Patient
                                            {
                                                IdPatients = p.IdPatients,
                                                Name = p.Name,
                                                Activo = p.Activo
                                            },
                                            FechaInicio = listado.FechaInicio,
                                            Price = e.Price,

                                            Consultorio = new Modelos.Consultorio
                                            {
                                                IdConsultorio = c.IdConsultorio,
                                                Nombre = c.Nombre,

                                            },

                                            Repetir = listado.Repetir,
                                            Frecuencia = listado.Frecuencia,
                                            Dias = listado.Dias,

                                            Recurrencia = new SqlTables.Recurrencium
                                            {
                                                IdRecurrencia = (int)listado.IdRecurrencia
                                            }
                                        };

                        olista.AddRange(resultEva.Distinct().ToList());


                    }

                }

            }
            catch (Exception ex)
            {
               // _logger.LogInformation("el error en las citas es :", ex);
                return ex;
            }
            return olista;
        }


    }
}
