using System;
using System.Collections.Generic;

namespace Clinica.SqlTblas
{
    public partial class Inversion
    {
        public int? IdAccounting { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateOfInvestment { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
