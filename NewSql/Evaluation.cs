using System;
using System.Collections.Generic;

namespace Clinica.NewSql
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            Recurrencia = new HashSet<Recurrencium>();
        }

        public int Id { get; set; }
        public int? IdPatients { get; set; }
        public int? IdTherapy { get; set; }
        public int? Price { get; set; }
        public int? IdTerapeuta { get; set; }
        public bool? Visitas { get; set; }
        public int? IdConsultorio { get; set; }
        public int? FirstPrice { get; set; }

        public virtual Consultorio? IdConsultorioNavigation { get; set; }
        public virtual ICollection<Recurrencium> Recurrencia { get; set; }
    }
}
