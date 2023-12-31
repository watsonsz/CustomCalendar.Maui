﻿using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using CustomCalendar.BusinessEntity;
using CustomCalendar.CustomViews;
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
            this.EmployeeDatePicked = date;
            InitializeDayViews(MonthDateTime);
            var _empRepo = new EmployeeRepository();
            this.Employees = _empRepo.GetAllEmployees();
            //Need to Get All Employees
            ShiftSelect = 1;
        }
        #endregion

        public Guid Id { get; set; }
        public DateTime MonthDateTime { get; set; }
        public string CurrentMonth {  get; set; }
        public List<DayViewModel> Days { get; set; }
        public List<EmployeeEntity> Employees { get; set; }


        public int ShiftSelect { get; set; }


        public DateTime EmployeeDatePicked { get; set; }
        public EmployeeEntity SelectedEmployee { get; set; }
        public string ErrorMessage { get; internal set; }

        #region Methods
        private void InitializeDayViews(DateTime selectedMonth)
        {
            var _repo = new DataAccessHelper();
            var daysResults = _repo.BuildDays(Id);

            //need at this point to get lists
            
            Days = new List<DayViewModel>();
            if (daysResults == null) 
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

        public bool AddEmployee(bool acknowledged)
        {
            foreach(var day in Days)
            {
                if(day.Date == EmployeeDatePicked)
                {
                   //bool assigned = day.CheckIfAssigned(SelectedEmployee.Id);
                   bool assigned = false;
                    //need to define a shift

                    if (assigned && !acknowledged)
                    {
                        ErrorMessage = "This Person has already been assigned to a shift this day";
                        return true;
                    }
                    switch (ShiftSelect)
                    {
                        case 1:
                            day.Firstshiftemployees.Add(SelectedEmployee);
                            SelectedEmployee = null;
                            break;
                        case 2:
                            day.Secondshiftemployees.Add(SelectedEmployee);
                            SelectedEmployee = null;
                            break;
                        case 3:
                            day.Lastshiftemployees.Add(SelectedEmployee);
                            SelectedEmployee = null;
                            break;
                    }
                    
                }
               
            }
            return false;
        }

        [RelayCommand]
        void SetShiftSelect(string shiftSelect)
        {
            var shiftChoice = Int32.Parse(shiftSelect);
            ShiftSelect = shiftChoice;
        }

        public void SaveDays()
        {
            DataAccessHelper _newRepo = new DataAccessHelper();

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
            _newRepo.SaveDays(daysList);
        }

        #endregion
    }
}
