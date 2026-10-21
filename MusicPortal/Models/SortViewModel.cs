namespace MusicPortal.Models
{
    public class SortViewModel
    {
        public SortState Name { get; private set; }
        public SortState Songwriter { get; private set; }
        public SortState Current {  get; private set; }

        public SortViewModel(SortState sortState)
        {
            Name = sortState == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            Songwriter = sortState == SortState.SongwriterAsc ? SortState.SongwriterDesc : SortState.SongwriterAsc;
            Current = sortState;
        }
    }
}
