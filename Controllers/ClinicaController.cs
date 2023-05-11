using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clinica.Modelos;
using Clinica.SqlTblas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Nest;
using static Nest.JoinField;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        public readonly dbapiContext _dbcontext;
        private readonly string cadenaSQL;
        public ClinicaController(dbapiContext _context, IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Patient> Lista = new List<Patient>();
            try
            {
                Lista = _dbcontext.Patients.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
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
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("EditarTerapia")]
        public IActionResult EditarTerapia([FromBody] Therapy objeto)
        {
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
                _dbcontext.Therapies.Update(oProducto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarAdmin")]
        public IActionResult EditarAdmin([FromBody] SqlTblas.User objeto)
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
        public IActionResult Asistencias([FromBody] Attendance attendance)
        {
            _dbcontext.Attendances.Add(attendance);
            _dbcontext.SaveChanges();
            return Ok("Juan");
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


        //  <----------------------------- filtrar citas -------------------------> 


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
        [Route("GetEvaluacionByTerapeuta")]
        public async Task<IActionResult> GetEvaluacionByTerapeuta(IdtherapistIdtherapy terapeutaId)
        {
            List<Probar> viewModal = new List<Probar>();
            var evaluaciones = await _dbcontext.IdtherapistIdtherapies.Where(e => e.Idterapeuta == terapeutaId.Idterapeuta).ToListAsync();

            if (evaluaciones != null)
            {
                foreach (var cita in evaluaciones)
                {
                    var idsTerapia = cita.Idtherapia;
                    var terapia = await FiltrarTerapia(idsTerapia);

                    Probar nuevoObjeto = new Probar();
                    nuevoObjeto.NombreTerapia = terapia;
                    viewModal.Add(nuevoObjeto);
                }
            }

            return Ok(viewModal);
        }


        [HttpPost]
        [Route("BuscarPacientePorTerapeuta")]
        public async Task<IActionResult> BuscarPacientePorTerapeuta(IdtherapistIdtherapy terapeutaId)
        {
            try
            {
                List<Probar> viewModal = new List<Probar>();
                var evaluaciones = await _dbcontext.Evaluations.Where(e => e.IdTerapeuta == terapeutaId.Idterapeuta).ToListAsync();

                if (evaluaciones != null)
                {
                    foreach (var cita in evaluaciones)
                    {
                        var idsTerapia = cita.IdPatients;
                        var terapia = await Filtrar(idsTerapia);

                        Probar nuevoObjeto = new Probar();
                        nuevoObjeto.NombrePaciente = terapia;
                        viewModal.Add(nuevoObjeto);
                    }
                }

                return Ok(viewModal);
            }
            catch(Exception ex) {
                return Ok(ex);
            }
              
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
        [Route("FiltrarGastos")]
        public async Task<IActionResult> FiltrarGastos(Inversion obj)
        {
            string mensaje = string.Empty;
            List<Inversion> viewModal = new List<Inversion>();
            var gastos = _dbcontext.Inversions.Where(x => x.DateOfInvestment >= obj.DateOfInvestment && x.DateOfInvestment < obj.EndDate).ToList();


            if(gastos.Count != 0)
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



        [HttpPost]
        [Route("GastosGanancia")]
        public async Task<IActionResult> GastosGanancia(Attendance obj)
        {

            Attendance fechaInicio = new  Attendance();

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
            catch(Exception ex) {
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
        [Route("AgregarEvento")]
        public IActionResult AgregarEvento([FromBody] Attendance agenda)
        {
            _dbcontext.Attendances.Add(agenda);
            _dbcontext.SaveChanges();
            return Ok();
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
    }
}



