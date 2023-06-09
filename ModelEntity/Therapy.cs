﻿using System;
using System.Collections.Generic;

namespace Clinica.ModelEntity
{
    public partial class Therapy
    {
        public int IdTherapy { get; set; }
        public string? Label { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int? Porcentaje { get; set; }
        public int? PorcentajeCentro { get; set; }
        public bool? Activo { get; set; }
    }
}
