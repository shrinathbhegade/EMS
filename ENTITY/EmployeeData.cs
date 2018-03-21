
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
   public class EmployeeData
    {
        public List<EmployeeEntity> Employees { get; set; }
        public List<EmployeeLookup> Designations { get; set; }
        public List<EmployeeLookup> Departments { get; set; }
        public List<EmployeeLookup> Supervisors { get; set; }
        public EmployeeEntity Entity { get; set; }
    }
}
