using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace KendoGridAssignment.Models
{
    public class Employee
    {
        //[Required(ErrorMessage ="Please enter address")]
        public string ADDRESS { get; set; }

        //[StringLength(10,ErrorMessage ="Contact number should be 10 digit long", MinimumLength =10)]
        //[Required(ErrorMessage = "Please provide contact no.")]
        //[RegularExpression("^[0-9]*$",ErrorMessage ="Only numbers are allowed")]      
        public string CONTACT_NUMBER { get; set; }

        //[DataType(DataType.DateTime)]

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [ValidBirthDate]
        [Required(ErrorMessage = "Select Date of birth")]
        [DisplayName("birth date")]
        public DateTime? DATE_OF_BIRTH { get; set; }

        public int DEPARTMENT_ID { get; set; }
        public int DESIGNATION_ID { get; set; }

        //[Required(ErrorMessage = "Please enter email id")]
        public string EMAIL_ID { get; set; }

        [Required(ErrorMessage = "Please enter experience")]
        [Range(0.0, 50.00, ErrorMessage = "Experience should be less than 50.01")]
        public double EXPERIENCE { get; set; }

        // [Required(ErrorMessage ="Please select gender")]
        public string GENDER { get; set; }
        public string INSERTED_BY { get; set; }
        public DateTime? INSERTED_ON { get; set; }



        //[Required(ErrorMessage = "Name is not entered")]
        //[StringLength(10,ErrorMessage ="Min length is 3, Max length is 10",MinimumLength =3)]
        //[RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Alphabets allowed.")]
        public string NAME { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Skill is requierd")]
        [RegularExpression("[a-zA-Z]+(\\s+[a-zA-Z]+)*", ErrorMessage = "Enter only text, Spaces are not allowed at ends")]
        public string SKILLS { get; set; }
        public int? SUPERVISOR_ID { get; set; }


        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_ON { get; set; }
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select designation")]
        public string DESIGNATION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter department")]
        public string DEPARTMENT { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select supervisor")]
        public string SUPERVISOR { get; set; }

    }
}
