 using System.ComponentModel.DataAnnotations.Schema;

namespace CustomCalendar.BusinessEntity
{
    public class DaysEntity
    {
        public DateTime DayDatetime { get; set; }

        [ForeignKey(nameof(MonthEntity))]
        public Guid MonthId { get; set; }
        public MonthEntity Month { get; set; }

        public List<EmployeeShiftEntity> FirstShift { get; set; }
        public List<EmployeeShiftEntity> SecondShift { get; set; }
        public List<EmployeeShiftEntity> ThirdShift { get; set; }
        public List<EmployeeShiftEntity> HouseKeeping { get; set; }
        public List<EmployeeShiftEntity> KitchenStaff { get; set; }

    }
}