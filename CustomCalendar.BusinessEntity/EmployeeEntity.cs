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
        public EmployeeEntity(string name)
        {
            Id = Guid.NewGuid();
            this.Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    
    }
}
