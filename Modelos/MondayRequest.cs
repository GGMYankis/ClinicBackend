namespace Clinica.Modelos
{
    public class MondayRequest
    {
        public DateTime StartDate { get; set; }
        public List<DayOfWeek> TargetDay { get; set; }
    }
}
