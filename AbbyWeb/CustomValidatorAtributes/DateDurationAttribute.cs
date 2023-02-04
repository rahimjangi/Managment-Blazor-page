using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AbbyWeb.CustomValidatorAtributes
{
    public class DateDurationAttribute:ValidationAttribute
    {
        public string OtherProperty { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            {
                if (value != null)
                {

                    PropertyInfo? fromDatePropertyName = validationContext.ObjectType.GetProperty(OtherProperty);
                    var valueFromDate = fromDatePropertyName.GetValue(validationContext.ObjectInstance);

                    if (valueFromDate != null)
                    {
                        DateTime toDate = Convert.ToDateTime(value);
                        DateTime fromDate = Convert.ToDateTime(valueFromDate);
                        if (toDate.CompareTo(fromDate) >= 0)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            return new ValidationResult(ErrorMessage);
                        }
                    }

                    return null;
                }
                return null;
            }
        }
    }
}
