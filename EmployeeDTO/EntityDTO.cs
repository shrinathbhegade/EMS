using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EntityDTO
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public int DESIGNATION_ID { get; set; }
        public string DESIGNATION { get; set; }
        public string GENDER { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT { get; set; }
        public double EXPERIENCE { get; set; }
        public string SKILLS { get; set; }
        public string EMAIL_ID { get; set; }
        public string CONTACT_NUMBER { get; set; }
        public int? SUPERVISOR_ID { get; set; }
        public string SUPERVISOR { get; set; }
        public DateTime DATE_OF_BIRTH { get; set; }
        public string ADDRESS { get; set; }
        public string INSERTED_BY { get; set; }
        public DateTime? INSERTED_ON { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_ON { get; set; }

    }
}
