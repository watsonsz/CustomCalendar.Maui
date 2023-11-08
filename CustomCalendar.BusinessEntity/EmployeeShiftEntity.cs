using System.ComponentModel.DataAnnotations.Schema;

namespace CustomCalendar.BusinessEntity
{
    public class EmployeeShiftEntity
    {
        public int ShiftType {  get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime DayDatetime { get; set; }
    }
}