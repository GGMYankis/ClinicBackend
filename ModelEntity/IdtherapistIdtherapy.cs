﻿using System;
using System.Collections.Generic;

namespace Clinica.ModelEntity
{
    public partial class IdtherapistIdtherapy
    {
        public int Id { get; set; }
        public int? Idterapeuta { get; set; }
        public int? Idtherapia { get; set; }
    }
}
