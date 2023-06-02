namespace Clinica.Modelos
{
    public class ListaFecha
    {
       
        public int IdAsistencias { get; set; }
        public int? IdTerapeuta { get; set; }
        public int? IdPatients { get; set; }
        public int? IdTherapy { get; set; }
        public DateTime? fechas { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string? TipoAsistencias { get; set; }
        public string? Remarks { get; set; }
    }
}
