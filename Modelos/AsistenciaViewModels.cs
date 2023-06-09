﻿namespace Clinica.Modelos
{
    public class AsistenciaViewModels
    {
        public int IdAsistencias { get; set; }
        public int? IdTerapeuta { get; set; }
        public int? IdPatients { get; set; }
        public int? IdTherapy { get; set; }
        public List<DateTime> FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? TipoAsistencias { get; set; }
        public string? Remarks { get; set; }
    }
}
