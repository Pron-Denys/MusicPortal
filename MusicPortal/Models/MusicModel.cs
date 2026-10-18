namespace MusicPortal.Models
{
    using System.ComponentModel.DataAnnotations;
    public class MusicModel
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Name { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "ReleaseYear")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public int Year { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "Songwriter")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Songwriter { get; set; }
        [Display(ResourceType = typeof(Resources.Resource),Name = "Genre")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Genre { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "ChooseSong")]
        public string Audio { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "ChoosePoster")]
        public string Poster { get; set; }
    }
}
