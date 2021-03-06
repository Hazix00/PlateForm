using PlateForm.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PlateForm.Core.ValidationAttributes
{
    public class Ticket_EnsureDueDatePresentAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if (!ticket.ValidateDueDatePresence())
                return new ValidationResult("Due date is required.");

            return ValidationResult.Success;
        }
    }
}
