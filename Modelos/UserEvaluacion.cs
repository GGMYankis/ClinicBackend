using Clinica.NewSql;

namespace Clinica.Modelos
{
    public class UserEvaluacion
    {

        public User? Terapeuta { get; set; }
        public Patient? Paciente { get; set; }
        public Therapy? Terapia { get; set; }
        public int? IdEvaluacion { get; set; }
        public Consultorio? Consultorio { get; set; }
        public int? Price { get; set; }
        public int? FirstPrice { get; set; }
        public int? IdConsultorio { get; set; }
        
        public DateTime? FechaInicio { get; set; }
        public int? Repetir { get; set; }
        public string? Frecuencia { get; set; }
        public string? Dias { get; set; }
        public List<string>? DiasUi { get; set; }
        public Recurrencium? Recurrencia { get; set; }

        
    }
}
