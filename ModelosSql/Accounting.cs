using System;
using System.Collections.Generic;

namespace Clinica.ModelosSql
{
    public partial class Accounting
    {
        public int IdAccounting { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateOfInvestment { get; set; }
    }
}
