using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.Validator
{
    public class descriptionMinimumWords: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;
            fieldValue.Trim();

            if (fieldValue == null || fieldValue.Split().Length < 5)
            {
                return new ValidationResult("La descrizione deve avere almeno 5 parole");
            }

            return ValidationResult.Success;
        }
    }
}
