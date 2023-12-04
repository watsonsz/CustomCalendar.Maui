using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class EmployeeManagementViewModel: ObservableObject
    {
        private EmployeeRepository _repo;
        public EmployeeManagementViewModel()
        {
            _repo = new EmployeeRepository();
            ActiveEmployees = new ObservableCollection<EmployeeEntity>();
            InactiveEmployees = new ObservableCollection<EmployeeEntity>();
            PopulateAllEmployees();
        }

        private void PopulateAllEmployees()
        {
            var list = _repo.GetAllEmployees();
            foreach (var employee in list)
            {
                if (employee.IsActive)
                {
                    ActiveEmployees.Add(employee);
                }
                else
                {
                    InactiveEmployees.Add(employee);
                }
            }
        }
        [ObservableProperty]
        EmployeeEntity selectedEmployee;

        [ObservableProperty]
        ObservableCollection<EmployeeEntity> activeEmployees;

        [ObservableProperty]
        ObservableCollection<EmployeeEntity> inactiveEmployees;

        #region FormFields
        [ObservableProperty]
        string fullName;

        [ObservableProperty]
        string phoneNumber;

        [ObservableProperty]
        string address;

        [ObservableProperty]
        bool hasPTO;

        [ObservableProperty]
        bool wantsMoreHours;

        [ObservableProperty]
        bool isActive;
        #endregion

        [RelayCommand]
        private void SaveEmployee()
        {
            //create new employee
            var newEmployee = new EmployeeEntity();
            newEmployee.FullName = FullName;
            newEmployee.PhoneNumber = PhoneNumber;
            newEmployee.Address = Address;
            newEmployee.HasPTO = HasPTO;
            newEmployee.WantsMoreHours = WantsMoreHours;
            newEmployee.IsActive = IsActive;
           
            //save them
            _repo.SaveEmployee(newEmployee);
            if (newEmployee.IsActive)
            {
                ActiveEmployees.Add(newEmployee);
            }
            else
            {
                InactiveEmployees.Add(newEmployee);
            }
            

            //Remove Fields
            FullName = "";
            PhoneNumber = "";
            Address = "";
            HasPTO = false;
            IsActive = false;
            WantsMoreHours = false;
        }

        [RelayCommand]
        private void UpdateEmployee(EmployeeEntity employee)
        {

        }

        internal void AddToActive(string v)
        {
            foreach(var entity in InactiveEmployees.ToList())
            {
                if (entity.Id.ToString() == v)
                {
                    entity.IsActive = true;
                    _repo.UpdateEmployee(entity);
                    ActiveEmployees.Add(entity);
                    InactiveEmployees.Remove(entity);
                }
            }
        }

        internal void AddToInactive(string v)
        {
            foreach (var entity in ActiveEmployees.ToList())
            {
                if (entity.Id.ToString() == v)
                {
                    entity.IsActive = false;
                    _repo.UpdateEmployee(entity);
                    InactiveEmployees.Add(entity);
                    ActiveEmployees.Remove(entity);
                }
            }
        }
    }
}
