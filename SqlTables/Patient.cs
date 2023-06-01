using System;
using System.Collections.Generic;

namespace Clinica.SqlTables
{
    public partial class Patient
    {
        public int IdPatients { get; set; }
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public string? ParentsName { get; set; }
        public string? ParentOrGuardianPhoneNumber { get; set; }
        public string? NumberMothers { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal? Age { get; set; }
        public string? EducationalInstitution { get; set; }
        public string? Course { get; set; }
        public string? WhoRefers { get; set; }
        public string? FamilySettings { get; set; }
        public string? TherapiesOrServiceYouWillReceiveAtTheCenter { get; set; }
        public string? Diagnosis { get; set; }
        public string? Recommendations { get; set; }
        public string? FamilyMembersConcerns { get; set; }
        public string? SpecificMedicalCondition { get; set; }
        public string? Other { get; set; }
        public bool? Activo { get; set; }
        public string? FechaIngreso { get; set; }
    }
}
