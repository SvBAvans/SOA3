namespace Cinema
{
    public class Movie
    {
        public string Title { get; set; }
        public List<MovieScreening> Screenings = new List<MovieScreening>();

        public Movie(string title)
        {
            Title = title;
        }

        public void AddScreening(MovieScreening screening)
        {
            Screenings.Add(screening);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
