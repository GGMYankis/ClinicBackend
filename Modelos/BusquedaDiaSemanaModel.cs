namespace Clinica.Modelos
{
    public class BusquedaDiaSemanaModel
    {
        public List<DayOfWeek> DiasSemana { get; set; }
        public DateTime Fecha { get; set; }

        public DateTime? FechaInicio { get; set; }
        public int Repetir { get; set; }
        public string? Frecuencia { get; set; }
        public string? Dias { get; set; }
        public int? IdEvaluation { get; set; }
    }

}
