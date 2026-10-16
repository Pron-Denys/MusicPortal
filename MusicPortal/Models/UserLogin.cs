using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class UserLogin
    {
        [Display(Name = "Логін")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Login { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Заповніть поле")]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Довжина пароля від 8 до 12 символів")]
        public string? Password { get; set; }
    }
}
