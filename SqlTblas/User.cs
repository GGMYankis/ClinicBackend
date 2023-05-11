using System;
using System.Collections.Generic;

namespace Clinica.SqlTblas
{
    public partial class User
    {
        public int? IdUser { get; set; }
        public string? Names { get; set; }
        public string? Label { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? IdRol { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
    }
}
