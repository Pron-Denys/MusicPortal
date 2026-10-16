namespace MusicPortal.Models
{
    using System.ComponentModel.DataAnnotations;
    public class MusicModel
    {
        public int Id { get; set; }
        [Display(Name = "Назва пісні")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Name { get; set; }
        [Display(Name = "Рік випуску")]
        [Required(ErrorMessage = "Заповніть поле")]
        public int Year { get; set; }
        [Display(Name = "Виконавець")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string? Songwriter { get; set; }
        [Display(Name = "Жанр")]
        [Required(ErrorMessage = "Оберіть жанр пісні")]
        public string? Genre { get; set; }
        public string Audio { get; set; }
        public string Poster { get; set; }
    }
}
