using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Helpers
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly DateOnly minDate;
        private readonly DateOnly maxDate;
        public DateRangeAttribute()
        {
            minDate = DateOnly.FromDateTime(DateTime.Now);
            maxDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(3));

            if (string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = $"Дата должна быть от {minDate.ToShortDateString()} до {maxDate.ToShortDateString()}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult(ErrorMessage);
            
            var today = (DateOnly)value;

            if(today < minDate || today > maxDate)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
