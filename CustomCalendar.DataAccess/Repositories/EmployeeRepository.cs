using CustomCalendar.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.DataAccess.Repositories
{
    public class EmployeeRepository
    {
        private string connectionString;
        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<EmployeeEntity> GetEmployeesFromList(List<EmployeeShiftEntity> employeeList)
        {
            throw new NotImplementedException();
        }

        internal EmployeeEntity GetEmployeeById(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
