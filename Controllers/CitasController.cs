using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clinica.ModelEntity;
using Clinica.Modelos;
using Microsoft.EntityFrameworkCore;
using Nest;

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

        [HttpPost]
        [Route("PacientesTerapeuta")]
        public IActionResult PacientesTerapeuta([FromBody] User obj)
        {

            List<Evaluation> Citas = new List<Evaluation>();
            List<Patient>? Paciente = new List<Patient>();

            using (var context = _dbcontext)
            {
                var result = from c in context.Evaluations
                             where c.IdTerapeuta == obj.IdUser
                             select new Evaluation
                             {
                                 IdPatients = c.IdPatients
                             };

                Citas.AddRange(result);


                foreach (var listado in Citas)
                {
                    var paciente = from p in context.Patients
                                   where p.IdPatients == listado.IdPatients
                                   select new Patient
                                   {
                                       IdPatients = p.IdPatients,
                                       Name = p.Name,
                                       Sex = p.Sex,
                                       ParentsName = p.ParentsName,
                                       ParentOrGuardianPhoneNumber = p.ParentOrGuardianPhoneNumber,
                                       NumberMothers = p.NumberMothers,
                                       DateOfBirth = p.DateOfBirth,
                                       Age = p.Age,
                                       EducationalInstitution = p.EducationalInstitution,
                                       Course = p.Course,
                                       WhoRefers = p.WhoRefers,
                                       FamilySettings = p.FamilySettings,
                                       TherapiesOrServiceYouWillReceiveAtTheCenter = p.TherapiesOrServiceYouWillReceiveAtTheCenter,
                                       Diagnosis = p.Diagnosis,
                                       Recommendations = p.Recommendations,
                                       FamilyMembersConcerns = p.FamilyMembersConcerns,
                                       SpecificMedicalCondition = p.SpecificMedicalCondition,
                                       Other = p.Other,
                                       Activo = p.Activo,
                                       FechaIngreso = p.FechaIngreso
                                   };


                       Paciente.AddRange(paciente.ToList());


                }

            }

            return Ok(Paciente);

        }

        [HttpGet]
        [Route("CitasNoUnicas")]
        public async Task<object> CitasNoUnicas()
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

                

                    foreach (var listado in recu)
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
                                            FirstPrice = e.FirstPrice,

                                            Consultorio = new Modelos.Consultorio
                                            {
                                                IdConsultorio = c.IdConsultorio,
                                                Nombre = c.Nombre,

                                            },

                                            Repetir = listado.Repetir,
                                            Frecuencia = listado.Frecuencia,
                                            Dias = listado.Dias,

                                            Recurrencia = new ModelEntity.Recurrencium
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


        [HttpGet]
        [Route("Citas")]
        public async Task<object> Citas()
        {
            string mensaje = string.Empty;
            List<Evaluation> viewModal = new List<Evaluation>();
            List<UserEvaluacion> olista = new List<UserEvaluacion>();
            List<Recurrencia> recurrencia = new List<Recurrencia>();
            List<Recurrencia> recurrenciaProcesada = new List<Recurrencia>();

            try
            {
                using (var dbContext = _dbcontext)
                {
                    var result = from r in dbContext.Recurrencia

                                 select new Modelos.Recurrencia
                                 {
                                     FechaInicio = r.FechaInicio,
                                     Dias = r.Dias,
                                     IdEvaluation = r.IdEvaluation,
                                     IdRecurrencia = r.IdRecurrencia
                                 };

                    recurrencia = await result.ToListAsync();


                    foreach (var pro in recurrencia)
                    {                   
                        var idEvaluacion = pro.IdEvaluation;

                        Recurrencia recuProcesada = recurrenciaProcesada.FirstOrDefault(f => f.IdEvaluation == idEvaluacion);


                        if (recuProcesada == null)
                        {
                            recurrenciaProcesada.Add(pro);
                            string dia = pro.Dias;
                            pro.DiasA.Add(dia);
                        }
                        else
                        {
                            string dia = pro.Dias;

                            recuProcesada.DiasA.Add(dia);
                        }
                       
                    }


                    foreach (var listado in recurrenciaProcesada)
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
                                            FirstPrice = e.FirstPrice,

                                            Consultorio = new Modelos.Consultorio
                                            {
                                                IdConsultorio = c.IdConsultorio,
                                                Nombre = c.Nombre,

                                            },

                                            Repetir = listado.Repetir,
                                            Frecuencia = listado.Frecuencia,
                                            DiasUi = listado.DiasA,

                                            Recurrencia = new ModelEntity.Recurrencium
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
