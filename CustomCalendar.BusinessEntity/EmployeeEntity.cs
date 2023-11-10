using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.BusinessEntity
{
    public class EmployeeEntity
    {
        public EmployeeEntity() { Id = Guid.NewGuid(); }
        public EmployeeEntity(string name, Guid id)
        {
            Id = id;
            this.FullName = name;
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool HasPTO { get; set; }
        public bool WantsMoreHours { get; set; }
        public bool IsActive { get; set; }

    }
}
