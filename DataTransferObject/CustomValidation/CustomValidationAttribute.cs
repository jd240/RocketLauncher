using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public class RequireAtLeastOneAdditionalFieldAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("Invalid request body.");

        var properties = validationContext.ObjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        bool hasAdditionalValue = properties
            .Where(p => p.Name != nameof(DataTransferObject.DTO.UserUpdateRequest.UserId))
            .Any(p =>
            {
                var propValue = p.GetValue(value);

                if (propValue == null) return false;

                if (propValue is string str)
                    return !string.IsNullOrWhiteSpace(str);

                // Non-null value types (bool, enum, DateTime?, Guid?) count
                return true;
            });

        if (!hasAdditionalValue)
        {
            return new ValidationResult(
                "At least one field other than UserId must be supplied.",
                properties.Select(p => p.Name)
            );
        }

        return ValidationResult.Success;
    }
}