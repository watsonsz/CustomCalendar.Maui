 using System.ComponentModel.DataAnnotations.Schema;

namespace CustomCalendar.BusinessEntity
{
    public class DaysEntity
    {
        public DateTime DayDatetime { get; set; }

        [ForeignKey(nameof(MonthEntity))]
        public Guid MonthId { get; set; }
        public MonthEntity Month { get; set; }

        public List<EmployeeEntity> FirstShift { get; set; }
        public List<EmployeeEntity> SecondShift { get; set; }
        public List<EmployeeEntity> ThirdShift { get; set; }
        public List<EmployeeEntity> HouseKeeping { get; set; }
        public List<EmployeeEntity> KitchenStaff { get; set; }

    }
}