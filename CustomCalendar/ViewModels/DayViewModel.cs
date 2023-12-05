using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomCalendar.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.ViewModels
{
    public partial class DayViewModel: ObservableObject
    {
        public DayViewModel(DateTime date)
        {
            this.Date = date;
            //allEmployees = employeeBL.GetAll();
            DAYOFWEEKNUMBER = (int)Date.DayOfWeek;
            
        }
        public string DayLabel { get => GetLabel();}

        public DateTime Date { get; set; }
       
        public int DAYOFWEEKNUMBER { get; set; }

        public EmployeeEntity DaySelectedEmployee { get; set; }

        //These need to be done with employees instead of strings

        [ObservableProperty]
        public ObservableCollection<EmployeeEntity> firstshiftemployees = new ObservableCollection<EmployeeEntity>();

        [ObservableProperty]
        public ObservableCollection<EmployeeEntity> secondshiftemployees = new ObservableCollection<EmployeeEntity>();

        [ObservableProperty]
        public ObservableCollection<EmployeeEntity> lastshiftemployees = new ObservableCollection<EmployeeEntity>();

        [ObservableProperty]
        public ObservableCollection<EmployeeEntity> kitchenstaff = new ObservableCollection<EmployeeEntity>();

        [ObservableProperty]
        public ObservableCollection<EmployeeEntity> housekeeping = new ObservableCollection<EmployeeEntity>();

        public string GetLabel()
        {
            var day = Date.DayOfWeek.ToString();
            var shortMonthName = Date.ToString("MMM");
            var label = $"{shortMonthName} {Date.Day}";
            return label;
        }

        [RelayCommand]
        void RemoveEmployee()
        {
            var removedEmployee = DaySelectedEmployee;
            CheckEmployee(Firstshiftemployees, removedEmployee);
            CheckEmployee(Secondshiftemployees, removedEmployee);
            CheckEmployee(Lastshiftemployees, removedEmployee);
            CheckEmployee(Kitchenstaff, removedEmployee);
            CheckEmployee(Housekeeping, removedEmployee);
        }

        private void CheckEmployee(ObservableCollection<EmployeeEntity> employeeList, EmployeeEntity removedEmployee)
        {
            foreach(var entity in employeeList)
            {
                if(entity.Id == removedEmployee.Id)
                {
                    employeeList.Remove(entity);
                    break;
                }
            }
        }

        internal bool CheckIfAssigned(Guid id)
        {
            var masterList = Firstshiftemployees;
            bool assigned = false;
            foreach(var employee in Secondshiftemployees)
            {
                masterList.Add(employee);
            }
            foreach(var employee in Lastshiftemployees)
            {
                masterList.Add(employee);
            }
            foreach (var employee in Kitchenstaff) { masterList.Add(employee); }
            foreach( var employee in Housekeeping) { masterList.Add(employee); }

            foreach (var employee in masterList)
            {
                if (employee.Id == id)
                {
                    assigned = true;
                }
                else { assigned = false; }
            }

            return assigned;
        }
    }
}
