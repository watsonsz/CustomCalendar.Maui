using CommunityToolkit.Mvvm.Input;
using CustomCalendar.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.ViewModels
{
    public partial class MonthViewViewModel
    {
        #region Constructors
        public MonthViewViewModel()
        {
            this.MonthDateTime = DateTime.Now;
            this.CurrentMonth = DateTime.Now.ToString("MMMM");
            InitializeDayViews(this.MonthDateTime);
            Employees = new List<EmployeeEntity> { new EmployeeEntity("Zack"), new EmployeeEntity("Talaine") };
        }

        public MonthViewViewModel(int month)
        {
            //chnage this to month and year, just to be safe.
            EmployeeDatePicked = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime selectedMonth = new DateTime(DateTime.Now.Year, month, 1);
            this.CurrentMonth = selectedMonth.ToString("MMMM");
            this.MonthDateTime = selectedMonth;
            InitializeDayViews(selectedMonth);
            Employees = new List<EmployeeEntity> { new EmployeeEntity("Zack"), new EmployeeEntity("Talaine") };
        }
        #endregion

        public DateTime MonthDateTime { get; set; }
        public string CurrentMonth {  get; set; }
        public List<DayViewModel> Days { get; set; }
        public List<EmployeeEntity> Employees { get; set; }


        public DateTime EmployeeDatePicked { get; set; }
        public EmployeeEntity SelectedEmployee { get; set; }

        #region Methods
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

        [RelayCommand]
        void AddEmployee()
        {
            foreach(var day in Days)
            {
                if(day.Date == EmployeeDatePicked)
                {
                    //need to define a shift
                    day.Firstshiftemployees.Add(SelectedEmployee);
                }
            }
        }

        #endregion
    }
}
