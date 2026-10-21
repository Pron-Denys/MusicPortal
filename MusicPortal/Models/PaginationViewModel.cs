namespace MusicPortal.Models
{
    public class PaginationViewModel
    {
        public int TotalPages { get; set; }
        public int Page {  get; set; }
        public bool HasPreviosPage { get; set; }
        public bool HasNextPage { get; set; }

        public PaginationViewModel(int page, int count, decimal maxCountPage)
        {
            Page = page;
            TotalPages = (int)Math.Ceiling(count / maxCountPage);
            HasPreviosPage = Page > 1;
            HasNextPage = Page < TotalPages;
        }
    }
}
