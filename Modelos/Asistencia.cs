using Clinica.ModelEntity;

namespace Clinica.Modelos
{
    public class Asistencia
    {

        public User Therapeua { get; set; }
        public Patient Pacientes { get; set; }
        public Therapy Terapias { get; set; }
        public Attendance Asistencias { get; set; }
        public string TipoAsistencia { get; set; }
    }
}
