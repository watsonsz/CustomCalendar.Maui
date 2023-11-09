using CommunityToolkit.Mvvm.Input;
using CustomCalendar.BusinessEntity;
using CustomCalendar.DataAccess.Repositories;
using CustomCalendar.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            
        }

        public MonthViewViewModel(Guid id, DateTime date, string monthName)
        {
            this.Id = id;
            this.MonthDateTime = date;
            this.CurrentMonth = monthName;
            InitializeDayViews(MonthDateTime);
            //Need to Get All Employees
        }
        #endregion

        public Guid Id { get; set; }
        public DateTime MonthDateTime { get; set; }
        public string CurrentMonth {  get; set; }
        public List<DayViewModel> Days { get; set; }
        public List<EmployeeEntity> Employees { get; set; }


        public DateTime EmployeeDatePicked { get; set; }
        public EmployeeEntity SelectedEmployee { get; set; }

        #region Methods
        private void InitializeDayViews(DateTime selectedMonth)
        {
            var _repo = new DataAccessHelper();
            var daysResults = _repo.BuildDays(Id);

            //need at this point to get lists
            
            Days = new List<DayViewModel>();
            if(daysResults.Count <= 1)
            {
                CreateNewDays(selectedMonth);
            }
            else
            {
                Days = daysResults;
            }
               
        }

        

        private void CreateNewDays(DateTime selectedMonth)
        {
            int GetDaysInMonth = DateTime.DaysInMonth(selectedMonth.Year, selectedMonth.Month);
            for (int i = 1; i <= GetDaysInMonth; i++)
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

        public void SaveDays()
        {
            DayRepository _repo = new DayRepository("CHANGE");
            List<DaysEntity> daysList = new List<DaysEntity>();
            foreach (var day in Days)
            {
                var newDay = new DaysEntity
                {
                    DayDatetime = day.Date,
                    MonthId = this.Id,
                    FirstShift = day.Firstshiftemployees.ToList(),
                    SecondShift = day.Secondshiftemployees.ToList(),
                    ThirdShift = day.Lastshiftemployees.ToList(),
                    HouseKeeping = day.Housekeeping.ToList(),
                    KitchenStaff = day.Kitchenstaff.ToList(),
                };
                daysList.Add(newDay);
            }
            _repo.SaveDays(daysList);
        }
        #endregion
    }
}
