using Newtonsoft.Json;

namespace Cinema.Domain
{
    public class Movie
    {
        public string Title { get; set; }
        [JsonIgnore]
        public List<MovieScreening> Screenings { get; } = new List<MovieScreening>();

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
