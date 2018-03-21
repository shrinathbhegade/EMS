using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoGridAssignment.Models
{
    public class DataModel
    {
        public List<Employee> employees { get; set; }
        public List<Lookup> designations { get; set; }
        public List<Lookup> departments { get; set; }
        public List<Lookup> supervisors { get; set; }
        public Employee employee { get; set; }
        public DataModel()
        {
            employee = new Employee { DATE_OF_BIRTH = null };
        }
    }
}