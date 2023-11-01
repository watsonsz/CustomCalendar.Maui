using CustomCalendar.DataAccess.Repositories;
using CustomCalendar.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCalendar.BusinessEntity;

namespace CustomCalendar.BusinessLogic
{
    public class MonthBL
    {
        public MonthBL()
        {
            
        }
        MonthRepository _monthRepo = new MonthRepository();

        public void SaveCalendar(MonthViewViewModel month)
        {
            MonthEntity monthEntity = new MonthEntity();
        }
    }
}
