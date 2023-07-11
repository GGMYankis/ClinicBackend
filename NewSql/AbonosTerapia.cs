using System;
using System.Collections.Generic;

namespace Clinica.NewSql
{
    public partial class AbonosTerapia
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdTerapia { get; set; }
        public int? IdTerapeuta { get; set; }
        public int? IdPaciente { get; set; }
        public decimal? MontoPagado { get; set; }
    }
}
