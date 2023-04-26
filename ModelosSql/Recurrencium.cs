using System;
using System.Collections.Generic;

namespace Clinica.ModelosSql
{
    public partial class Recurrencium
    {
        public int IdRecurrencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public int? Repetir { get; set; }
        public string? Frecuencia { get; set; }
        public string? Dias { get; set; }
        public int? IdEvaluation { get; set; }

        public virtual Evaluation? IdEvaluationNavigation { get; set; }
    }
}
