using System;
using System.Collections.Generic;

namespace Clinica.ModelEntity
{
    public partial class TipoAsistencia
    {
        public TipoAsistencia()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
