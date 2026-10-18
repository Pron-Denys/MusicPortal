using System.ComponentModel.DataAnnotations;
namespace MusicPortal.Models
{
    public class UserLogin
    {
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Login { get; set; }
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorLenPassword")]
        public string? Password { get; set; }
    }
}
