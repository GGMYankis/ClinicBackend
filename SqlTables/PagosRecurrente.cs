using System;
using System.Collections.Generic;

namespace Clinica.SqlTables
{
    public partial class PagosRecurrente
    {
        public int? IdTherapia { get; set; }
        public int? IdPaciente { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
