using System.ComponentModel.DataAnnotations.Schema;

namespace CustomCalendar.BusinessEntity
{
    public class EmployeeShiftEntity
    {
        public int Id { get; set; }
        public int ShiftType {  get; set; }

        public Guid EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; }

        public DateTime DayDatetime { get; set; }
        public DaysEntity Day { get; set; }
    }
}