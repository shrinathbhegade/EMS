using System;
using System.ComponentModel.DataAnnotations;


namespace KendoGridAssignment.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidBirthDateAttribute : ValidationAttribute
    {
        public ValidBirthDateAttribute()
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime _birthJoin = Convert.ToDateTime(value);
                if (_birthJoin > DateTime.Now)
                {
                    return new ValidationResult("Birth date can not be greater than current date.");
                }
            }
            return ValidationResult.Success;
        }

    }
}