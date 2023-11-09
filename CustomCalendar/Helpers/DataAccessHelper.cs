using CustomCalendar.BusinessEntity;
using CustomCalendar.DataAccess.Repositories;
using CustomCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomCalendar.Helpers
{
    public class DataAccessHelper
    {
        public DayRepository _dayRepo;
        public EmployeeRepository _employeeRepo;
        public ShiftLinkingRepository _shiftRepo;
        private string connectionstring;
        private const int FIRST_SHIFT = 1;
        private const int SECOND_SHIFT = 2;
        private const int LAST_SHIFT = 3;
        private const int HOUSEKEEPING = 4;
        private const int KITCHEN = 5;

        public DataAccessHelper()
        {
            connectionstring = "Data Source=localhost;Initial Catalog=CustomCalendar;Integrated Security=True";
            _dayRepo = new DayRepository(connectionstring);
            _employeeRepo = new EmployeeRepository(connectionstring);
            _shiftRepo = new ShiftLinkingRepository(connectionstring);
        }



        public List<DayViewModel> BuildDays(Guid MonthId)
        {
            List<DayViewModel> viewModelList = new List<DayViewModel>();
            //GetDays
            var days = _dayRepo.GetDaysForMonth(MonthId);
            //GetShifts
            foreach(var day in days)
            {
                var employeeList = _shiftRepo.GetEmployeeShiftEntities(day.DayDatetime, FIRST_SHIFT);
                day.FirstShift = _employeeRepo.GetEmployeesFromList(employeeList);

                employeeList = _shiftRepo.GetEmployeeShiftEntities(day.DayDatetime, SECOND_SHIFT);
                day.SecondShift = _employeeRepo.GetEmployeesFromList(employeeList);

                employeeList = _shiftRepo.GetEmployeeShiftEntities(day.DayDatetime, LAST_SHIFT);
                day.ThirdShift = _employeeRepo.GetEmployeesFromList(employeeList);

                employeeList = _shiftRepo.GetEmployeeShiftEntities(day.DayDatetime, HOUSEKEEPING);
                day.HouseKeeping = _employeeRepo.GetEmployeesFromList(employeeList);

                employeeList = _shiftRepo.GetEmployeeShiftEntities(day.DayDatetime, KITCHEN);
                day.KitchenStaff = _employeeRepo.GetEmployeesFromList(employeeList);

            }

            //Create ViewModel
            foreach (var day in days)
            {
                var newDay = new DayViewModel(day.DayDatetime)
                {
                    Firstshiftemployees = GenerateObservableCollection(day.FirstShift),
                    Secondshiftemployees = GenerateObservableCollection(day.SecondShift),
                    Lastshiftemployees = GenerateObservableCollection(day.ThirdShift),
                    Housekeeping = GenerateObservableCollection(day.HouseKeeping),
                    Kitchenstaff = GenerateObservableCollection(day.KitchenStaff)
                };
                viewModelList.Add(newDay);
            }
            return viewModelList;
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
    }
}
