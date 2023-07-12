using Clinica.Modelos;
using Clinica.ModelEntity;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Nest;
using System;
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

        [HttpPost]
        [Route("EditarRecurrencia")]
        public IActionResult EditarRecurrencia([FromBody] Recurrencia obj)
        {
            try
            {
                var id = obj.IdRecurrencia;

                var oProducto = _dbcontext.Recurrencia.Where(r => r.IdEvaluation == obj.IdEvaluation).ToList();
                foreach (var numero in oProducto)
                {
                    _dbcontext.Recurrencia.Remove(numero);
                    _dbcontext.SaveChanges();
                }

                foreach (var numero in obj.DiasA)
                {
                    Recurrencium recu = new Recurrencium();
                    recu.Dias = numero;
                    recu.FechaInicio = obj.FechaInicio;
                    recu.Repetir = obj.Repetir;
                    recu.IdEvaluation = obj.IdEvaluation;
                    recu.Frecuencia = obj.Frecuencia;

                    _dbcontext.Recurrencia.Add(recu);
                    _dbcontext.SaveChanges();
                }

                return Ok();

            }
            catch (Exception ex) {
                return  BadRequest();
            }

        }

        [HttpPost]
        [Route("CrearEvaluacion")]
        public IActionResult CrearEvaluacion([FromBody] Citas objeto)
        {
            List<Evaluation> olista = new List<Evaluation>();

            try
            {
                string MensajeError = string.Empty;

                var CitaExistente = _dbcontext.Evaluations.Where(cita => 
                   cita.IdPatients == objeto.IdPatients && cita.IdTerapeuta == objeto.IdTerapeuta &&
                   cita.IdTherapy == objeto.IdTherapy && cita.IdConsultorio == objeto.IdConsultorio
                );

                olista = CitaExistente.ToList();


                 foreach(var obj in olista)
                {
                    Recurrencium recuExistente = _dbcontext.Recurrencia.FirstOrDefault(recu => recu.IdEvaluation == obj.Id);

                    if (recuExistente.FechaInicio == objeto.FechaInicio)
                    {
                        return BadRequest(MensajeError = "Ya existe un registro con esta información");
                    }
                }

                 if(objeto.Price == null)
                {
                    Therapy terapia = _dbcontext.Therapies.FirstOrDefault(t => t.IdTherapy == objeto.IdTherapy);


                    if(terapia != null)
                    {
                        objeto.Price = terapia.Price;
                    }

                }

                Evaluation datos = new Evaluation()
                {
                    IdPatients = objeto.IdPatients,
                    IdTherapy = objeto.IdTherapy,
                    Price = objeto.Price,
                    FirstPrice = objeto.FirstPrice,
                    IdTerapeuta = objeto.IdTerapeuta,
                    Visitas = objeto.Visitas,
                    IdConsultorio = objeto.IdConsultorio
                };

                 _dbcontext.Evaluations.Add(datos);
                 _dbcontext.SaveChanges();

                var idObtenido = datos.Id;
                if (idObtenido > 0 && objeto.Dias.Count > 0)
                {
                    foreach (var numero in objeto.Dias)
                    {
                        Recurrencium recu = new Recurrencium();
                        recu.Dias = numero;
                        recu.FechaInicio = objeto.FechaInicio;
                        recu.IdEvaluation = idObtenido;

                        //   recu.Repetir = objeto.Repetir;
                        // recu.Frecuencia = objeto.Frecuencia;

                        _dbcontext.Recurrencia.Add(recu);
                         _dbcontext.SaveChanges();
                    }
                    return Ok();
                }
                else
                {
                    Evaluation remover = _dbcontext.Evaluations.FirstOrDefault(e => e.Id == idObtenido);
                    _dbcontext.Evaluations.Remove(remover);
                    _dbcontext.SaveChanges();   
                    return BadRequest(MensajeError = "Error al crear un cita");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }




    }
}

