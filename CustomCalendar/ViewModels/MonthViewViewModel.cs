using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.ViewModels
{
    public class MonthViewViewModel
    {
        public MonthViewViewModel()
        {
            this.MonthDateTime = DateTime.Now;
            this.CurrentMonth = DateTime.Now.ToString("MMMM");
            InitializeDayViews(this.MonthDateTime);
        }

        public MonthViewViewModel(int month)
        {
            //chnage this to month and year, just to be safe.
            DateTime selectedMonth = new DateTime(DateTime.Now.Year, month, 1);
            this.CurrentMonth = selectedMonth.ToString("MMMM");
            this.MonthDateTime = selectedMonth;
            InitializeDayViews(selectedMonth);
        }

        public DateTime MonthDateTime { get; set; }
        public string CurrentMonth {  get; set; }
        public List<DayViewModel> Days { get; set; }

        private void InitializeDayViews(DateTime selectedMonth)
        {
            Days = new List<DayViewModel>();
            int GetDaysInMonth = DateTime.DaysInMonth(selectedMonth.Year, selectedMonth.Month);
            for(int i = 1; i <= GetDaysInMonth; i++)
            {
                var selectedDate = new DateTime(selectedMonth.Year, selectedMonth.Month, i);
                var newDay = new DayViewModel(selectedDate);
                Days.Add(newDay);
                
            }
               
        }
    }
}
