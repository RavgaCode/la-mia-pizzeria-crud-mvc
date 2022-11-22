using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.Validator
{
    public class isNumberValid : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            double fieldValue = (double)value;

            if (fieldValue == null || fieldValue <= 0)
            {
                return new ValidationResult("Il prezzo è obbligatorio e deve essere superiore a 0");
            }

            return ValidationResult.Success;
        }
    }
}
