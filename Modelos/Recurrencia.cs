﻿using Clinica.NewModels;

namespace Clinica.Modelos
{
    public class Recurrencia
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
