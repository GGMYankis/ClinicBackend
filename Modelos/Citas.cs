namespace Clinica.Modelos
{
    public class Citas
    {
        // EVALUACION
        public int Id { get; set; }
        public int? IdPatients { get; set; }
        public int? IdTherapy { get; set; }
        public int? Price { get; set; }
        public int? IdTerapeuta { get; set; }
        public bool? Visitas { get; set; }
        public int? IdConsultorio { get; set; }

        //Recurrencia

        public int IdRecurrencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public int? Repetir { get; set; }
        public string? Frecuencia { get; set; }
         public List<string>? Dias { get; set; }
         public List<string>? DiasA { get; set; }
        
        public int? IdEvaluation { get; set; }

    }
}
