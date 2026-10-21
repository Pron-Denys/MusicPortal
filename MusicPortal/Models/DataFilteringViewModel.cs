using Microsoft.AspNetCore.Mvc.Rendering;
namespace MusicPortal.Models
{
    public class DataFilteringViewModel
    {
        public string? Name { get; set; }
        public SelectList Genres { get; set; }
        public Genre SelectedGenre { get; set; }

        public DataFilteringViewModel(string name, List<Genre> genres, Genre selectedGenre)
        {
            Name = name;
            var temp = new Genre { Name = "Всі" };
            genres.Insert(0, temp);
            if (selectedGenre == null)
                SelectedGenre = temp;
            else
                SelectedGenre = selectedGenre;              
            Genres = new SelectList(genres, "Id", "Name", SelectedGenre);
        }

    }
}
