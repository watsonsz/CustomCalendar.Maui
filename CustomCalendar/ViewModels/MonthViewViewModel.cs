using CommunityToolkit.Mvvm.Input;
using CustomCalendar.BusinessEntity;
using CustomCalendar.DataAccess.Repositories;
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
            var _repo = new DayRepository();
            var daysResults = _repo.GetDaysForMonth(Id);
            //need at this point to get lists
            
            Days = new List<DayViewModel>();
            if(daysResults.Count <= 1)
            {
                CreateNewDays(selectedMonth);
            }
            else
            {
                foreach(var day in daysResults)
                {
                    var newDay = new DayViewModel(day.DayDatetime)
                    {
                        Firstshiftemployees = GenerateObservableCollection(day.FirstShift),
                        Secondshiftemployees= GenerateObservableCollection(day.SecondShift),
                        Lastshiftemployees = GenerateObservableCollection(day.ThirdShift),
                        Housekeeping = GenerateObservableCollection(day.HouseKeeping),
                        Kitchenstaff = GenerateObservableCollection(day.KitchenStaff)
                    };
                    Days.Add(newDay);
                }
            }
               
        }

        private ObservableCollection<EmployeeEntity> GenerateObservableCollection(List<EmployeeEntity> employees)
        {
            var returnList = new ObservableCollection<EmployeeEntity>();
            foreach (var employee in employees)
            {
                returnList.Add(employee);
            }
            return returnList;
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

        #endregion
    }
}
