namespace MusicPortal.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Genre
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Name { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "ChoosePoster")]
        public string? Poster { get; set; }
        public ICollection<Music>? Musics { get; set; }
    }
}
