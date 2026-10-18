using System.ComponentModel.DataAnnotations;
namespace MusicPortal.Models
{
    public class RegistrationModel
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "FirstName")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Name { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "Surname")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Surname { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "HowOldAreYou")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public int Year { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Login { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorLenPassword")]
        public string? Password { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "PasswordСonfirmation")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordsDoNotMatch")]
        public string? PasswordConfirm { get; set; }
    }
}
