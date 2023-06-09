﻿using System;
using System.Collections.Generic;

namespace Clinica.ModelEntity
{
    public partial class Rol
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public int IdRol { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
