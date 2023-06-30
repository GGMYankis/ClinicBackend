using Clinica.SqlTables;

namespace Clinica.Modelos
{
    public class Asistencia
    {

        public User Therapeua { get; set; }
        public Patient Pacientes { get; set; }
        public Therapy Terapias { get; set; }
        public Attendance Asistencias { get; set; }
    }
}
