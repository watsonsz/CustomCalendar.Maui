using System.ComponentModel.DataAnnotations.Schema;

namespace CustomCalendar.BusinessEntity
{
    public class EmployeeShiftEntity
    {
        public int Id { get; set; }
        public int ShiftType {  get; set; }
        [ForeignKey(nameof(EmployeeEntity))]
        public Guid EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; }

        [ForeignKey(nameof(DaysEntity))]
        public DateTime DayDatetime { get; set; }
        public DaysEntity Day { get; set; }
    }
}