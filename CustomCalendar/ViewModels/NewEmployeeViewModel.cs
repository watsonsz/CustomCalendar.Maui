using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomCalendar.BusinessEntity;
using CustomCalendar.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.ViewModels
{
    public partial class NewEmployeeViewModel: ObservableObject
    {
        private EmployeeRepository _repo;
        public NewEmployeeViewModel()
        {
            _repo = new EmployeeRepository();
        }

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

            //Remove Fields
            FullName = "";
            PhoneNumber = "";
            Address = "";
            HasPTO = false;
            IsActive = false;
            WantsMoreHours = false;
        }
    }
}
