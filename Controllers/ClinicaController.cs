using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clinica.Modelos;
using Clinica.NewSql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data; 
using Microsoft.AspNetCore.Authorization;
using Nest;
using static Nest.JoinField;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlClient;
using System.Linq;
using Serilog;
using System.Security.Cryptography;

namespace Clinica.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        public readonly dbapiContext _dbcontext;
        private readonly string cadenaSQL;
        private readonly ILogger<ClinicaController> _logger;

        public ClinicaController(dbapiContext _context, IConfiguration config, ILogger<ClinicaController> logger)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
            _dbcontext = _context;
            _logger = logger;

        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Patient> Lista = new List<Patient>();
            try
            {
                Lista = _dbcontext.Patients.Where(p => p.Activo == true).ToList();
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpGet]
        [Route("ListaTodos")]
        public IActionResult ListaTodos()
        {
            List<Patient> Lista = new List<Patient>();
            try
            {
                Lista = _dbcontext.Patients.ToList();
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }
        [HttpGet]
        [Route("Listas")]
        public IActionResult Listas()
        {
            List<IdtherapistIdtherapy> Lista = new List<IdtherapistIdtherapy>();
            try
            {
                Lista = _dbcontext.IdtherapistIdtherapies.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpGet]
        [Route("ListaUsers")]
        public IActionResult ListaUsers()
        {
            List<User> Lista = new List<User>();
            try
            {
                Lista = _dbcontext.Users.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpPost]
        [Route("GuardarPaciente")]
        public IActionResult Guardar([FromBody] Patient objeto)
        {
            try
            {
                objeto.FechaIngreso = DateTime.Now;

              _dbcontext.Patients.Add(objeto);
              _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("ContabilidadReportes")]
        public IActionResult ContabilidadReportes([FromBody] Inversion objeto)
        {
            try
            {
                _dbcontext.Inversions.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarPaciente")]
        public IActionResult EditarPaciente([FromBody] Patient objeto)
        {

            Patient oProducto = _dbcontext.Patients.Find(objeto.IdPatients);

            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }

            try
            {

                oProducto.Name = objeto.Name is null ? oProducto.Name : objeto.Name;
                oProducto.Sex = objeto.Sex is null ? oProducto.Sex : objeto.Sex;
                oProducto.ParentsName = objeto.ParentsName is null ? oProducto.ParentsName : objeto.ParentsName;
                oProducto.ParentOrGuardianPhoneNumber = objeto.ParentOrGuardianPhoneNumber is null ? oProducto.ParentOrGuardianPhoneNumber : objeto.ParentOrGuardianPhoneNumber;
                oProducto.NumberMothers = objeto.NumberMothers is null ? oProducto.NumberMothers : objeto.NumberMothers;
                oProducto.DateOfBirth = objeto.DateOfBirth is null ? oProducto.DateOfBirth : objeto.DateOfBirth;
                oProducto.Activo = objeto.Activo is null ? oProducto.Activo : objeto.Activo;
                oProducto.Age = objeto.Age is null ? oProducto.Age : objeto.Age;
                oProducto.EducationalInstitution = objeto.EducationalInstitution is null ? oProducto.EducationalInstitution : objeto.EducationalInstitution;
                oProducto.Course = objeto.Course is null ? oProducto.Course : objeto.Course;
                oProducto.WhoRefers = objeto.WhoRefers is null ? oProducto.WhoRefers : objeto.WhoRefers;
                oProducto.FamilySettings = objeto.FamilySettings is null ? oProducto.FamilySettings : objeto.FamilySettings;
                oProducto.TherapiesOrServiceYouWillReceiveAtTheCenter = objeto.TherapiesOrServiceYouWillReceiveAtTheCenter is null ? oProducto.TherapiesOrServiceYouWillReceiveAtTheCenter : objeto.TherapiesOrServiceYouWillReceiveAtTheCenter;
                oProducto.Diagnosis = objeto.Diagnosis is null ? oProducto.Diagnosis : objeto.Diagnosis;
                oProducto.Recommendations = objeto.Recommendations is null ? oProducto.Recommendations : objeto.Recommendations;
                oProducto.FamilyMembersConcerns = objeto.FamilyMembersConcerns is null ? oProducto.FamilyMembersConcerns : objeto.FamilyMembersConcerns;
                oProducto.SpecificMedicalCondition = objeto.SpecificMedicalCondition is null ? oProducto.SpecificMedicalCondition : objeto.SpecificMedicalCondition;
                oProducto.Other = objeto.Other is null ? oProducto.Other : objeto.Other;

                _dbcontext.Patients.Update(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("CrearTerapia")]
        public IActionResult CrearTerapia([FromBody] Therapy objeto)
        {
            try
            {
                _dbcontext.Therapies.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
               return BadRequest();
            }
        }

        [HttpPost]
        [Route("EditarTerapia")]
        public IActionResult EditarTerapia([FromBody] Therapy objeto)
        {
            var mensaje = string.Empty;

            Therapy oProducto = _dbcontext.Therapies.Find(objeto.IdTherapy);
            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            try
            {
                oProducto.Label = objeto.Label is null ? oProducto.Label : objeto.Label;
                oProducto.Description = objeto.Description is null ? oProducto.Description : objeto.Description;
                oProducto.Price = objeto.Price is null ? oProducto.Price : objeto.Price;
                oProducto.Porcentaje = objeto.Porcentaje is null ? oProducto.Porcentaje : objeto.Porcentaje;
                oProducto.PorcentajeCentro = objeto.PorcentajeCentro is null ? oProducto.PorcentajeCentro : objeto.PorcentajeCentro;
                _dbcontext.Therapies.Update(oProducto);
                _dbcontext.SaveChanges();
                 return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
             
            }
            catch (Exception ex)
            {
                return BadRequest(mensaje = "Hubo un error al crear la terapia");
            }
        }

        [HttpPut]
        [Route("EditarAdmin")]
        public IActionResult EditarAdmin([FromBody] NewSql.User objeto)
        {
            User oProducto = _dbcontext.Users.Find(objeto.IdUser);
            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            try
            {
                oProducto.Names = objeto.Names is null ? oProducto.Names : objeto.Names;
                oProducto.Email = objeto.Email is null ? oProducto.Email : objeto.Email;
                _dbcontext.Users.Update(oProducto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("Asistencias")]
        public IActionResult Asistencias([FromBody] AsistenciaViewModels obj)
        {
            string MensajeError = string.Empty;


            Evaluation CitasExistente = _dbcontext.Evaluations.FirstOrDefault(citas => 
            
             citas.IdPatients ==  obj.IdPatients && citas.IdTherapy == obj.IdTherapy &&
             citas.IdTerapeuta == obj.IdTerapeuta
             
            );

            if(CitasExistente == null)
            {
                return BadRequest(MensajeError = "No existe una cita creada con esta información");
            }



            foreach (var fecha in obj.FechaInicio)
                {

                Attendance asistencia = new Attendance()
                {
                    IdPatients = obj.IdPatients,
                    IdTerapeuta = obj.IdTerapeuta,
                    IdTherapy = obj.IdTherapy,
                    FechaInicio = fecha,
                    TipoAsistencias = obj.TipoAsistencias,
                    Remarks = obj.Remarks,

                };         

                _dbcontext.Attendances.Add(asistencia);
                _dbcontext.SaveChanges();
              }

        
            return Ok("Juan");
        }



        [HttpPost]
        [Route("fechas")]
        public IActionResult Fechas([FromBody] ListaFecha objeto)
        {



            return Ok();

        }



        [HttpGet]
        [Route("Calendario")]
        public IActionResult Calendario()
        {
            List<Attendance> Lista = new List<Attendance>();
            try
            {
                Lista = _dbcontext.Attendances.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }


        [HttpGet]
        [Route("Consultorios")]
        public IActionResult Consultorios()
        {
            List<NewSql.Consultorio> Lista = new List<NewSql.Consultorio>();
            try
            {
                Lista = _dbcontext.Consultorios.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        
        }


        [HttpGet]
        [Route("terapeuta")]
        public IActionResult terapeuta()
        {
            List<User> Lista = new List<User>();
            try
            {
                var usuarios = _dbcontext.Users.Where(u => u.IdRol == 2).ToList();
                return StatusCode(StatusCodes.Status200OK, new { usuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { Lista });
            }
        }

        [HttpPost]
        [Route("Fecha")]
        public IActionResult Fecha(Attendance IdAsistencias)
        {
            var evento = _dbcontext.Attendances.FirstOrDefault(e => e.IdAsistencias == IdAsistencias.IdAsistencias);
            if (evento == null)
            {
                return NotFound();
            }
            _dbcontext.Attendances.Remove(evento);
            _dbcontext.SaveChanges();
            return Ok();
        }

        //  <----------------------------- filtrar Moscu -------------------------> 

        [HttpGet]
        [Route("Moscu")]
        public IActionResult Moscu()
        {
            List<Therapy> Lista = new List<Therapy>();
            try
            {
                Lista = _dbcontext.Therapies.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public IActionResult CrearUsuario([FromBody] User objeto)
        {
            try
            {
                _dbcontext.Users.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("CrearConsultorio")]
        public IActionResult CrearConsultorio([FromBody] NewSql.Consultorio objeto)
        {
            try
            {
                _dbcontext.Consultorios.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("EliminarUsuario")]
        public IActionResult EliminarUsuario([FromBody] User objeto)
        {
            try
            {
                var usuarioEncontrado = _dbcontext.Users.FirstOrDefault(u => u.IdUser == objeto.IdUser);
                _dbcontext.Remove(usuarioEncontrado);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("EliminarConsultorio")]
        public IActionResult EliminarConsultorio([FromBody] NewSql.Consultorio objeto)
        {
            try
            {
                var usuarioEncontrado = _dbcontext.Consultorios.FirstOrDefault(u => u.IdConsultorio == objeto.IdConsultorio);
                _dbcontext.Remove(usuarioEncontrado);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("GuardarUsers")]
        public IActionResult GuardarUsers([FromBody] User objeto)
        {

            User oProducto = _dbcontext.Users.Find(objeto.IdUser);

            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            oProducto.Names = objeto.Names is null ? oProducto.Names : objeto.Names;
            oProducto.Apellido = objeto.Apellido is null ? oProducto.Apellido : objeto.Apellido;
            oProducto.Telefono = objeto.Telefono is null ? oProducto.Telefono : objeto.Telefono;
            oProducto.Direccion = objeto.Direccion is null ? oProducto.Direccion : objeto.Direccion;
            oProducto.Email = objeto.Email is null ? oProducto.Email : objeto.Email;
            oProducto.Password = objeto.Password is null ? oProducto.Password : objeto.Password;
            oProducto.IdRol = objeto.IdRol is null ? oProducto.IdRol : objeto.IdRol;

            _dbcontext.Users.Update(oProducto);
            _dbcontext.SaveChanges();
            return Ok();

        }

        [HttpPost]
        [Route("EditarConsultorio")]
        public IActionResult EditarConsultorio([FromBody] NewSql.Consultorio objeto)
        {

            NewSql.Consultorio oProducto = _dbcontext.Consultorios.Find(objeto.IdConsultorio);

            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            oProducto.Nombre = objeto.Nombre is null ? oProducto.Nombre : objeto.Nombre;
            oProducto.Descripcion = objeto.Descripcion is null ? oProducto.Descripcion : objeto.Descripcion;
          

            _dbcontext.Consultorios.Update(oProducto);
            _dbcontext.SaveChanges();
            return Ok();

        }


        [HttpGet]
        [Route("ListaTerapia")]
        public IActionResult ListaTerapia()
        {
            List<Probar> viewModal = new List<Probar>();
            try
            {
                var oLista = _dbcontext.Therapies.ToList();
                foreach (var cita in oLista)
                {
                    Probar nuevoObjetao = new Probar();
                    nuevoObjetao.NombreTerapia = cita;
                    viewModal.Add(nuevoObjetao);
                }

                return Ok(viewModal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, viewModal });
            }
        }


        [HttpPost]
        [Route("BuscarPacientePorTerapeuta")]
        public async Task<IActionResult> BuscarPacientePorTerapeuta(IdtherapistIdtherapy terapeutaId)
        {
            try
            {
                List<Probar> viewModal = new List<Probar>();
                var evaluaciones = await _dbcontext.Evaluations
                .Where(e => e.IdTerapeuta == terapeutaId.Idterapeuta)
                .Select(e => e.IdPatients) 
                .Distinct()
                .ToListAsync();

                            if (evaluaciones != null)
                            {
                               foreach (var cita in evaluaciones)
                               {
                                       var resPaciente = await Filtrar(cita);
                                      Probar nuevoObjeto = new Probar();
                                      nuevoObjeto.NombrePaciente = resPaciente;
                                     viewModal.Add(nuevoObjeto);
                               }
                            }

                    return Ok(viewModal);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

   


        [HttpPost]
        [Route("FiltrarGastos")]
        public async Task<IActionResult> FiltrarGastos(Inversion obj)
        {
            string mensaje = string.Empty;
            List<Inversion> viewModal = new List<Inversion>();
            var gastos = _dbcontext.Inversions.Where(x => x.DateOfInvestment >= obj.DateOfInvestment && x.DateOfInvestment < obj.EndDate).ToList();

            if (gastos.Count != 0)
            {
                foreach (var cita in gastos)
                {
                    Inversion inversion = new Inversion();
                    inversion.Descripcion = cita.Descripcion;
                    inversion.Nombre = cita.Nombre;
                    inversion.Amount = cita.Amount;
                    inversion.DateOfInvestment = cita.DateOfInvestment;
                    viewModal.Add(inversion);
                }
                return Ok(viewModal);
            }
            mensaje = "No hubo Inversión para esta fecha";
            return StatusCode(StatusCodes.Status200OK, new { mensaje = mensaje });
        }



        //aquiiiii ------------------>\

       

        [HttpPost]
        [Route("GastosGanancia")]
        public async Task<IActionResult> GastosGanancia(Attendance obj)
        {

            Attendance fechaInicio = new Attendance();

            List<Probar> viewModal = new List<Probar>();
            var attendanceList = _dbcontext.Attendances.Where(x => x.FechaInicio >= obj.FechaInicio && x.FechaInicio < obj.FechaFinal).ToList();
            foreach (var cita in attendanceList)
            {
                var idsTerapia = cita.IdTherapy;
                var fechas = cita.FechaInicio;
                var terapia = await FiltrarTerapia(idsTerapia);

                fechaInicio.FechaInicio = fechas;

                Probar nuevoObjeto = new Probar();
                nuevoObjeto.FechaInicio = fechaInicio;

                nuevoObjeto.NombreTerapia = terapia;

                viewModal.Add(nuevoObjeto);
            }
            return StatusCode(StatusCodes.Status200OK, new { viewModal });
        }

        [HttpPost]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar(Attendance obj)
        {

            try
            {
                List<Probar> viewModal = new List<Probar>();
                var attendanceList = _dbcontext.Attendances.Where(x => x.IdTerapeuta == obj.IdTerapeuta && x.FechaInicio >= obj.FechaInicio && x.FechaInicio < obj.FechaFinal).ToList();
                foreach (var cita in attendanceList)
                {
                    var ids = cita.IdPatients;
                    var idsTerapia = cita.IdTherapy;
                    var idTerapeuta = cita.IdTerapeuta;
                    var paciente = await Filtrar(ids);
                    var terapia = await FiltrarTerapia(idsTerapia);
                    var terapeuta = await FiltrarTerapeuta(idTerapeuta);
                    Probar nuevoObjeto = new Probar();
                    nuevoObjeto.NombrePaciente = paciente;
                    nuevoObjeto.NombreTerapia = terapia;
                    nuevoObjeto.NombreTerapeuta = terapeuta;
                    viewModal.Add(nuevoObjeto);
                }
                return Ok(viewModal);
            }
            catch (Exception ex)
            {
                return Ok("error");
            }

        }
        private async Task<Patient> Filtrar(int? ids)
        {
            var paciente = await _dbcontext.Patients.FindAsync(ids);
            return paciente;
        }
        private async Task<Therapy> FiltrarTerapia(int? idTerapia)
        {
            var Tera = await _dbcontext.Therapies.FindAsync(idTerapia);
            return Tera;
        }

        private async Task<User> FiltrarTerapeuta(int? idTerapeuta)
        {
            var Terapeuta = await _dbcontext.Users.FindAsync(idTerapeuta);
            return Terapeuta;
        }

        [HttpPost]
        [Route("TraerUsuario")]
        public IActionResult TraerUsuario([FromBody] User usuario)
        {
            try
            {
                var users = _dbcontext.Users.FirstOrDefault(u => u.IdUser == usuario.IdUser);
                return StatusCode(StatusCodes.Status200OK, new { users });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }


        [HttpPost]
        [Route("EliminarPaciente")]
        public IActionResult EliminarPaciente([FromBody] Patient IdPaciente)
        {
            var paciente = _dbcontext.Patients.FirstOrDefault(u => u.IdPatients == IdPaciente.IdPatients);
            if (paciente == null)
            {
                NoContent();
            }
            _dbcontext.Remove(paciente);
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("EliminarTerapia")]
        public IActionResult EliminarTerapia([FromBody] Therapy terapia)
        {
            var terapiaEncontrada = _dbcontext.Therapies.FirstOrDefault(u => u.IdTherapy == terapia.IdTherapy);
            if (terapiaEncontrada == null)
            {
                NoContent();
            }
            _dbcontext.Remove(terapiaEncontrada);
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("CrearAbono")]
        public IActionResult CrearAbono([FromBody] Abono objeto)
        {
            try
            {
                _dbcontext.Abonos.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("Probar")]
        public object Probar([FromBody] Buscar obj)
        {
            List<Buscar> lista = new List<Buscar>();

            try
            {
                using (var dbContext = _dbcontext)
                {
                    var result = from a in dbContext.Attendances
                                 join t in dbContext.Therapies on a.IdTherapy equals t.IdTherapy
                                 where a.FechaInicio >= obj.FechaInicio && a.FechaInicio <= obj.FechaFinal
                                 select new Buscar
                                 {
                                     Price = t.Price,
                                     Label = t.Label,
                                     FechaInicio = a.FechaInicio,
                                     FechaFinal = a.FechaFinal
                                 };

                    lista = result.ToList();
                }
            }
            catch (Exception ex)
            {
                lista = new List<Buscar>();
            }
            return lista;
        }


        [HttpPost]
        [Route("AbonoTerapias")]
        public IActionResult AbonoTerapias([FromBody] AbonosTerapia objeto)
        {
            try
            {
                _dbcontext.AbonosTerapias.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("EliminarCita")]
        public IActionResult EliminarCita([FromBody] Citas obj)
        {

            var resRecu = _dbcontext.Recurrencia.Where(u => u.IdEvaluation == obj.IdEvaluation).ToList();



            if (resRecu == null)
            {
                NoContent();
            }

            foreach (var numero in resRecu)
            {
                _dbcontext.Recurrencia.Remove(numero);
                _dbcontext.SaveChanges();

            }


            var resEva = _dbcontext.Evaluations.FirstOrDefault(u => u.Id == obj.IdEvaluation);

            if (resEva == null)
            {
                NoContent();
            }
            _dbcontext.Remove(resEva);
            _dbcontext.SaveChanges();
            
            return Ok();
        }
        [HttpPost]
        [Route("Post")]
        public IActionResult Post(ListaEnteros obj)
        {

            foreach (var numero in obj.teras)
            {
                _dbcontext.IdtherapistIdtherapies.AddRange(new IdtherapistIdtherapy { Idtherapia = numero, Idterapeuta = obj.id });
            }
            _dbcontext.SaveChanges();
            return Ok();
        }


        [HttpPost]
        [Route("EditarCitas")]
        public IActionResult EditarCitas([FromBody] Citas objeto )
        {
            try
            {
         
                Evaluation oProducto =  _dbcontext.Evaluations.Find(objeto.Id);

                if (oProducto == null)
                {
                    return BadRequest("producto no encontrado");
                }

                oProducto.IdPatients = objeto.IdPatients is null ? oProducto.IdPatients : objeto.IdPatients;
                oProducto.IdTherapy = objeto.IdTherapy is null ? oProducto.IdTherapy : objeto.IdTherapy;
                oProducto.Price = objeto.Price is null ? oProducto.Price : objeto.Price;
                oProducto.FirstPrice = objeto.FirstPrice is null ? oProducto.FirstPrice : objeto.FirstPrice;
                oProducto.IdTerapeuta = objeto.IdTerapeuta is null ? oProducto.IdTerapeuta : objeto.IdTerapeuta;
                oProducto.Visitas = objeto.Visitas is null ? oProducto.Visitas : objeto.Visitas;
                oProducto.IdConsultorio = objeto.IdConsultorio is null ? oProducto.IdConsultorio : objeto.IdConsultorio;

                _dbcontext.Evaluations.Update(oProducto);
                 _dbcontext.SaveChanges();
           
                return Ok();

            }catch(Exception ex)
            {
                return BadRequest();
            }


        }


        [HttpPost]
        [Route("GetEvaluacionByTerapeuta")]
        public async Task<IActionResult> GetEvaluacionByTerapeuta(IdtherapistIdtherapy terapeutaId)
        {
            List<IdtherapistIdtherapy> filtrada = new List<IdtherapistIdtherapy>();
            List<Therapy> therapies = new List<Therapy>();
            List<Therapy> distinctTherapies = new List<Therapy>();


            using (var dbContext = _dbcontext)
            {
                var resultEva = from e in dbContext.IdtherapistIdtherapies
                                where e.Idterapeuta == terapeutaId.Idterapeuta
                                select new IdtherapistIdtherapy
                                {
                                    Idtherapia = e.Idtherapia
                                };


                filtrada.AddRange(resultEva);


                foreach(var aProcesar in filtrada)
                {
                 
                    var idTerapia = aProcesar.Idtherapia;


                    var filtro = therapies.Find(t => t.IdTherapy == idTerapia);

                    if(filtro == null)
                    {
                        var res = from e in dbContext.Therapies
                                  join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                  where e.IdTherapy == idTerapia
                                  select new Therapy
                                  {
                                      IdTherapy = t.IdTherapy,
                                      Label = t.Label
                                  };

                        therapies.AddRange(res.ToList());
                    }

               
                };
            }

             return Ok(therapies);

            }


         [HttpPost]
        [Route("FiltrarConsultorios")]
        public object FiltrarConsultorios(Buscar obj)
        {
            string mensaje = string.Empty;
            List<Evaluation> viewModal = new List<Evaluation>();
            List<UserEvaluacion> olista = new List<UserEvaluacion>();
            List<Buscar> recu = new List<Buscar>();


            using (var dbContext = _dbcontext)
            {
                var result = from r in dbContext.Recurrencia
                             where r.FechaInicio >= obj.FechaInicio && r.FechaInicio <= obj.FechaFinal
                             select new Buscar
                             {
                                 FechaInicio = r.FechaInicio,
                                 Dias= r.Dias,  
                                 IdEvaluation = r.IdEvaluation
                             };

                recu = result.ToList();

                if(obj.IdConsultorio == 0)
                {
                    foreach (var listado in recu)
                    {
                        var idEva = listado.IdEvaluation;


                        var resultEva = from e in dbContext.Evaluations
                                        join c in dbContext.Consultorios on e.IdConsultorio equals c.IdConsultorio
                                        join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                        join p in dbContext.Patients on e.IdPatients equals p.IdPatients
                                        join u in dbContext.Users on e.IdTerapeuta equals u.IdUser
                                        where e.Id == idEva  && p.Activo == true
                                        select new Modelos.UserEvaluacion
                                        {
                                            IdEvaluacion = idEva,

                                            Terapeuta = new User
                                            {
                                                Names = u.Names,
                                                Apellido = u.Apellido
                                            },
                                            Terapia = new Therapy
                                            {
                                                Label = t.Label
                                            },
                                            Dias = listado.Dias,

                                            Paciente = new Patient
                                            {
                                                Name = p.Name
                                            },
                                            FechaInicio = listado.FechaInicio,

                                            Consultorio = new Modelos.Consultorio
                                            {
                                                Nombre = c.Nombre
                                            },
                                        };

                        olista.AddRange(resultEva.ToList());
                    }
                    return olista;
                }

                foreach (var listado in recu)
                {
                    var idEva = listado.IdEvaluation;


                    var resultEva = from e in dbContext.Evaluations
                                    join c in dbContext.Consultorios on e.IdConsultorio equals c.IdConsultorio
                                    join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                    join p in dbContext.Patients on e.IdPatients equals p.IdPatients
                                    join u in dbContext.Users on e.IdTerapeuta equals u.IdUser
                                    where e.Id == idEva && e.IdConsultorio == obj.IdConsultorio && p.Activo == true
                                    select new Modelos.UserEvaluacion
                                    {
                                        IdEvaluacion = idEva,

                                        Terapeuta = new User
                                        {
                                            Names = u.Names,
                                            Apellido = u.Apellido
                                        },
                                        Terapia = new Therapy
                                        {
                                            Label = t.Label
                                        },
                                      
                                        Paciente = new Patient
                                        {
                                            Name = p.Name
                                        },
                                        FechaInicio = listado.FechaInicio,

                                           Consultorio = new Modelos.Consultorio
                                           {
                                               Nombre = c.Nombre
                                           },
                                    };

                    olista.AddRange(resultEva.ToList());
                }

            }

            return olista;
        }


        
        [Route("ListaEvaluacions")]
        public object ListaEvaluacions([FromBody] Buscar obj)
        {
            List<Buscar> lista = new List<Buscar>();
            List<UserEvaluacion> olista = new List<UserEvaluacion>();
            try
            {
                using (var dbContext = _dbcontext)
                {
                    var result = from r in dbContext.Recurrencia
                                 where r.FechaInicio >= obj.FechaInicio && r.FechaInicio <= obj.FechaFinal
                                 select new Buscar
                                 {
                                     FechaInicio = r.FechaInicio,
                                     IdEvaluation = r.IdEvaluation
                                 };

                    lista = result.ToList();

                    foreach (var listado in lista)
                    {




                        var idEva = listado.IdEvaluation;

                      UserEvaluacion filtrarTerapeuta = olista.Find(t => t.Terapeuta.IdUser == obj.IdTerapeuta);

                        if (filtrarTerapeuta == null)
                        {
                            var resultEva = from e in dbContext.Evaluations
                                            join a in dbContext.Attendances on new { e.IdTerapeuta } equals new { a.IdTerapeuta }
                                            where e.Id == idEva && e.IdTerapeuta == obj.IdTerapeuta
                                            select new
                                            {
                                                Evaluation = e,
                                                Attendance = a
                                            };

                            var resultPatients = from r in resultEva
                                                 join p in dbContext.Patients on r.Attendance.IdPatients equals p.IdPatients
                                                 join t in dbContext.Therapies on r.Attendance.IdTherapy equals t.IdTherapy
                                                 join u in dbContext.Users on r.Attendance.IdTerapeuta equals u.IdUser
                                                 select new Modelos.UserEvaluacion
                                                 {

                                                     Paciente = p,
                                                     Terapia = t,
                                                     Terapeuta = u,
                                                     FechaInicio = r.Attendance.FechaInicio,
                                                     Price = r.Evaluation.Price
                                                 };

                            olista.AddRange(resultPatients.ToList());
                        }

                              
                    }



                }
            }
            catch (Exception ex)
            {
                lista = new List<Buscar>();
            }
                            return olista;
          
        }


        [HttpPost]
        [Route("ReportesPago")]
        public IActionResult ReportesPago([FromBody] Attendance obj)
        {
            List<Attendance>  olista = new List<Attendance>();  
            List<PagoTerapeuta>  olistaUI = new List<PagoTerapeuta>();
            List<PagoTerapeuta>  cola = new List<PagoTerapeuta>();
            decimal? abono = 0;
            //decimal? Cobrar;
            //var A_Favor = 0;

            using (var dbContext = _dbcontext)
            {

                if (obj.IdPatients == 0)
                {
                    var resultAll = from a in dbContext.Attendances
                                    where a.FechaInicio >= obj.FechaInicio && a.FechaInicio <= obj.FechaFinal
                                    orderby a.FechaInicio ascending
                                    select new Attendance
                                    {
                                        IdAsistencias = a.IdAsistencias,
                                        FechaInicio = a.FechaInicio,
                                        IdTerapeuta = a.IdTerapeuta,
                                        IdPatients = a.IdPatients,
                                        IdTherapy = a.IdTherapy
                                    };
                    olista = resultAll.ToList();
                }
                else
                {
                    var result = from a in dbContext.Attendances
                                 where a.FechaInicio >= obj.FechaInicio && a.FechaInicio <= obj.FechaFinal &&
                                 a.IdPatients == obj.IdPatients && a.TipoAsistencias == "1" || a.TipoAsistencias == "3"
                                 orderby a.FechaInicio ascending
                                 select new Attendance
                                 {
                                     IdAsistencias = a.IdAsistencias,
                                     FechaInicio = a.FechaInicio,
                                     IdTerapeuta = a.IdTerapeuta,
                                     IdPatients = a.IdPatients,
                                     IdTherapy = a.IdTherapy
                                 };
                    olista = result.ToList();

                }
                foreach (var asis in olista)
                {
               
                    var idAsis = asis.IdAsistencias;
                    var idPaciente = asis.IdPatients;
                    var idTerapia = asis.IdTherapy;
                    var fechaAgroup = asis.FechaInicio.ToString().Substring(0,2);
                    
                   
                    string formattedDate = asis.FechaInicio.Value.ToString("dd-MMM");
                    formattedDate = formattedDate.TrimEnd('.');

                   
                    var longitud = olista.Count;

                    var resultAbono = from ab in dbContext.AbonosTerapias
                                      join t in dbContext.Therapies on ab.IdTerapia equals t.IdTherapy
                                      where ab.Fecha >= obj.FechaInicio && ab.Fecha <= obj.FechaFinal &&
                                      ab.IdPaciente == idPaciente && ab.IdTerapia == idTerapia
                                     
                                      select new Abono
                                      {
                                          Monto = ab.MontoPagado,
                                          priceTerapia = (int)t.Price
                                      };


                    if (resultAbono.Any())
                    {
                        foreach (var abo in resultAbono)
                            {

                            var paciRepetido2 = olistaUI.Find(p => p.Paciente.IdPatients == idPaciente && p.Terapia.IdTherapy == idTerapia);

                            abono = 0;
                            abono = abo.Monto;

                        
                        };
                    }
                    else
                    {
                        abono = 0;
                    }

                  
                    var paciRepetido = olistaUI.Find(p => p.Paciente.IdPatients == idPaciente && p.Terapia.IdTherapy == idTerapia);
                    if(paciRepetido != null)
                    {
                        paciRepetido.fechas.Add("," + " " + formattedDate);
                        paciRepetido.APagar = paciRepetido.fechas.Count * paciRepetido.Terapia.Price;

                    }
                    else
                    {
                        var veses = _dbcontext.Attendances.Where(a => a.IdPatients == idPaciente && a.IdTherapy == idTerapia).ToList();
                        var cantidad = veses.Count();
                        fechaAgroup = "";
                        var resultAsis = from a in dbContext.Attendances
                                         join t in dbContext.Therapies on a.IdTherapy equals t.IdTherapy
                                         join p in dbContext.Patients on a.IdPatients equals p.IdPatients
                                         join u in dbContext.Users on a.IdTerapeuta equals u.IdUser
                                         where a.IdAsistencias == idAsis
                                         select new Modelos.PagoTerapeuta
                                         {

                                             asistencia = new Attendance
                                             {
                                                 IdAsistencias = a.IdAsistencias,
                                                 FechaInicio = a.FechaInicio

                                             },
                                             Paciente = new Patient
                                             {
                                                 IdPatients = p.IdPatients,
                                                 Name = p.Name

                                             },
                                             Terapia = new Therapy
                                             {
                                                 IdTherapy = t.IdTherapy,
                                                 Label = t.Label,
                                                 Description = t.Description,
                                                 Price = t.Price

                                             },
                                             Terapeuta = new User
                                             {
                                                 Names = u.Names,
                                                 Apellido = u.Apellido

                                             },
                                             fechas = new List<string>
                                             {
                                                 formattedDate
                                             },
                                             APagar = (fechaAgroup.Length == 0) ? t.Price : fechaAgroup.Length * t.Price,
                                             CantidadAsistencia = longitud,
                                             Abono = abono,

                                         };


                        olistaUI.AddRange(resultAsis.ToList());
                    }
                   
                }

            }

            return Ok(olistaUI);

        }

        [HttpGet]
        [Route("Listadoasistencia")]
        public IActionResult ListadoAsistencia()
        {

            List<Attendance> listado = new List<Attendance>();
            List<Asistencia> AttendanceUi = new List<Asistencia>();

            using (var dbcontext = _dbcontext)
            {


                    var result = from r in dbcontext.Attendances
                                 select r;


               listado = result.ToList();


                    foreach (var attendance in listado)
                    {


                        var IdAsistencias = attendance.IdAsistencias;

                        var resulProcedada = from e in dbcontext.Attendances
                                             join t in dbcontext.Therapies on e.IdTherapy equals t.IdTherapy
                                             join p in dbcontext.Patients on e.IdPatients equals p.IdPatients
                                             join u in dbcontext.Users on e.IdTerapeuta equals u.IdUser
                                             where e.IdAsistencias == IdAsistencias
                                             select new Modelos.Asistencia
                                             {
                                                 Therapeua = u,
                                                 Terapias = t,
                                                 Pacientes = p,
                                                 Asistencias = e
                                             };


                    AttendanceUi.AddRange(resulProcedada.ToList());

                }


            }

            return Ok(AttendanceUi);

        }


        [HttpPost]
        [Route("EditarAsistencia")]
        public IActionResult EditarAsistencia([FromBody] Attendance objeto)
        {

            Attendance oProducto = _dbcontext.Attendances.Find(objeto.IdAsistencias);
            if (oProducto == null)
            {
                return BadRequest("error al editar la terapia");
            }

            oProducto.IdTerapeuta = objeto.IdTerapeuta is null ? oProducto.IdTerapeuta : objeto.IdTerapeuta;
            oProducto.IdPatients = objeto.IdPatients is null ? oProducto.IdPatients : objeto.IdPatients;
            oProducto.IdTherapy = objeto.IdTherapy is null ? oProducto.IdTherapy : objeto.IdTherapy;
            oProducto.FechaFinal = objeto.FechaFinal is null ? oProducto.FechaFinal : objeto.FechaFinal;
            oProducto.TipoAsistencias = objeto.TipoAsistencias is null ? oProducto.TipoAsistencias : objeto.TipoAsistencias;
            oProducto.Remarks = objeto.Remarks is null ? oProducto.Remarks : objeto.Remarks;
            oProducto.FechaInicio = objeto.FechaInicio is null ? oProducto.FechaInicio : objeto.FechaInicio;

            _dbcontext.Attendances.Update(oProducto);
            _dbcontext.SaveChanges();

            return Ok();


        }

        [HttpPost]
        [Route("EliminarAsistencia")]
        public IActionResult EliminarAsistencia([FromBody] Attendance objeto)
        {

            Attendance oProducto = _dbcontext.Attendances.Find(objeto.IdAsistencias);
            if (oProducto == null)
            {
                return BadRequest("error al eliminar la terapia");
            }


            _dbcontext.Attendances.Remove(oProducto);
            _dbcontext.SaveChanges();

            return Ok();


        }


        [HttpPost]
        [Route("FiltrandoAsistencia")]
        public IActionResult FiltrandoAsistencia([FromBody] Attendance obj)
        {

            List<Attendance> listado = new List<Attendance>();
            List<Asistencia> AttendanceUi = new List<Asistencia>();

            using (var dbcontext = _dbcontext)
            {

                var result = from r in dbcontext.Attendances
                             where r.FechaInicio >= obj.FechaInicio && r.FechaInicio <= obj.FechaFinal
                             select r;

                listado = result.ToList();

                foreach (var attendance in listado)
                {

                    var IdAsistencias = attendance.IdAsistencias;

                    var resulProcedada = from e in dbcontext.Attendances
                                         join t in dbcontext.Therapies on e.IdTherapy equals t.IdTherapy
                                         join p in dbcontext.Patients on e.IdPatients equals p.IdPatients
                                         join u in dbcontext.Users on e.IdTerapeuta equals u.IdUser
                                         where e.IdAsistencias == IdAsistencias
                                         select new Modelos.Asistencia
                                         {
                                             Therapeua = u,
                                             Terapias = t,
                                             Pacientes = p,
                                             Asistencias = e
                                         };
                    AttendanceUi.AddRange(resulProcedada.ToList());
                }
            }
            return Ok(AttendanceUi);

        }


        [HttpGet]
        [Route("ConfigListado")]
        public IActionResult ConfigListado()
        {

            List<Config> olista = new List<Config>();



            olista = _dbcontext.Configs.ToList();
          

            return Ok(olista);
        }

        [HttpPost]
        [Route("Config")]
        public IActionResult Config([FromBody] Config obj)
        {
   
                _dbcontext.Configs.Add(obj);
                _dbcontext.SaveChanges();
         
            return Ok();
        }


        [HttpPost]
        [Route("ConfigEditar")]
        public IActionResult ConfigEditar([FromBody] Config obj)
        {


            Config config = _dbcontext.Configs.Find(obj.IdKey);

            if (config != null)
            {
                config.Key = obj.Key is null ? config.Key : obj.Key;
                config.Value = obj.Value is null ? config.Value : obj.Value;

                _dbcontext.Configs.Update(config);
                _dbcontext.SaveChanges();

            }


            return Ok();
        }

        [HttpPost]
        [Route("ConfigEliminar")]
        public IActionResult ConfigEliminar([FromBody] Config obj)
        {


            Config config = _dbcontext.Configs.Find(obj.IdKey);

       
            if(config != null)
            {

                _dbcontext.Configs.Remove(config);
                _dbcontext.SaveChanges();
            }
       
            return Ok();
        }


      
        [HttpGet]
        [Route("razon")]
        public IActionResult razon()
        {

            List<Zaronasistencium> olista = new List<Zaronasistencium>();

            olista = _dbcontext.Zaronasistencia.ToList();

            return Ok(olista);
        }


    }
}

