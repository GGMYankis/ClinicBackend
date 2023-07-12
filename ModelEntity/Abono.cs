using System;
using System.Collections.Generic;

namespace Clinica.ModelEntity
{
    public partial class Abono
    {
        public int IdBono { get; set; }
        public int? IdTherapy { get; set; }
        public int? priceTerapia { get; set; } 
        public int? IdPatients { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Monto { get; set; }
        public string? Description { get; set; }
    }
}
