using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.BusinessEntity
{
    public class MonthEntity
    {

        public MonthEntity()
        {
            MonthID = Guid.NewGuid();
        }
        public Guid MonthID { get; set; }
        public string Year { get; set; }
        public string MonthName { get; set; }
        public List<DaysEntity> Days { get; set; }
        public List<EmployeeEntity> Employees { get; set; }
    }
}
