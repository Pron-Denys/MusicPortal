namespace MusicPortal.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public ICollection<Music>? Musics { get; set; }
    }
}
