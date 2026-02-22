using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnlineShopApp.Helpers
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult("Номер телефона обязателен для заполнения");

            string phoneNumber = value.ToString().Trim();

            // Удаляем все символы, кроме цифр и знака +
            string cleanedNumber = Regex.Replace(phoneNumber, @"[^\d\+]", "");

            // Проверяем, что номер начинается с 7, 8 или +7
            if (!Regex.IsMatch(cleanedNumber, @"^(\+7|8|7)"))
                return new ValidationResult("Номер должен начинаться с +7, 7 или 8");

            // Проверяем общую длину (после нормализации должно быть 11 или 12 цифр для +7)
            if (cleanedNumber.StartsWith("+7"))
            {
                if (cleanedNumber.Length != 12) // +7XXXXXXXXXX
                {
                    return new ValidationResult("После +7 должно быть 10 цифр");
                }
            }
            else if (cleanedNumber.Length != 11) // 8XXXXXXXXXX или 7XXXXXXXXXX
            {
                return new ValidationResult("Номер должен содержать 11 цифр");
            }

            return ValidationResult.Success;
        }
    }
}
