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
        public EmployeeRepository()
        {
            connectionString = "Data Source=localhost;Initial Catalog=CustomCalendar;Integrated Security=True";
        }

        internal EmployeeEntity GetEmployeeById(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
