using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DataDTO
    {
        public List<EntityDTO> Employees { get; set; }
        public List<LookupDTO> Designations { get; set; }
        public List<LookupDTO> Departments { get; set; }
        public List<LookupDTO> Supervisors { get; set; }
        public EntityDTO Employee { get; set; }
    }
}
