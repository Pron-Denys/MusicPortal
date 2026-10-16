using System.ComponentModel.DataAnnotations;
namespace MusicPortal.Models
{
    public class RegistrationModel
    {
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Name { get; set; }
        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Surname { get; set; }
        [Display(Name = "Скільки вам років")]
        [Required(ErrorMessage = "Заповніть поле")]
        public int Year { get; set; }
        [Display(Name = "Логін")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Login { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Заповніть поле")]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Довжина пароля від 8 до 12 символів")]
        public string? Password { get; set; }
        [Display(Name = "Підтвердження паролю")]
        [Required(ErrorMessage = "Заповніть поле")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        public string? PasswordConfirm { get; set; }
    }
}
