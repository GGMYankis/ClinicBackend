using System;
using System.Collections.Generic;

namespace Clinica.SqlTblas
{
    public partial class Therapy2
    {
        public int IdTherapy { get; set; }
        public string? Value { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
    }
}
