using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Utils
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            if (value is DateTime birthDate)
            {
                var today = DateTime.Now;
                var minDate = today.AddYears(-100);

                return birthDate <= today && birthDate >= minDate;
            }

            return false;
        }
    }
}
