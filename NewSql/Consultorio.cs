using System;
using System.Collections.Generic;

namespace Clinica.NewSql
{
    public partial class Consultorio
    {
        public Consultorio()
        {
            Evaluations = new HashSet<Evaluation>();
        }

        public int IdConsultorio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }
    }
}
