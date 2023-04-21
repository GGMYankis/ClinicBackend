using System;
using System.Collections.Generic;

namespace Clinica.TdTablas
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            Recurrencia = new HashSet<Recurrencium>();
        }

        public int Id { get; set; }
        public string? IdPatients { get; set; }
        public string? IdTherapy { get; set; }
        public string? Price { get; set; }
        public int? IdTerapeuta { get; set; }
        public bool? Visitas { get; set; }
        public DateTime? FechaInicio { get; set; }
        public int? Repetir { get; set; }
        public string? Frecuencia { get; set; }
        public string? Dias { get; set; }

        public virtual ICollection<Recurrencium> Recurrencia { get; set; }
    }
}
