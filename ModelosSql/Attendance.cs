using System;
using System.Collections.Generic;

namespace Clinica.ModelosSql
{
    public partial class Attendance
    {
        public int IdAsistencias { get; set; }
        public int? IdTerapeuta { get; set; }
        public int? IdPatients { get; set; }
        public int? IdTherapy { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string? Remarks { get; set; }
    }
}
