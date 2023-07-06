using Clinica.Msql;

namespace Clinica.Modelos
{
    public class PagoTerapeuta
    {
        public User? Terapeuta { get; set; }
        public Patient? Paciente { get; set; }
        public Therapy? Terapia { get; set; }
        public Attendance? asistencia { get; set; }
        public List<string>? fechas { get; set; }
        public int? APagar { get; set; }
        public int? CantidadAsistencia { get; set; }
        public decimal? Abono { get; set; }
        public decimal? Cobrar { get; set; }
    }
}
