using FluentValidation.Results;

namespace Deliver.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> VallationErrors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            VallationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                VallationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
