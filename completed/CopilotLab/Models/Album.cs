using System;

namespace CopilotLab
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; } // Changed from string to Genre

        public Album(int id, string title, string artist, DateTime releaseDate, Genre genre)
        {
            Id = id;
            Title = title;
            Artist = artist;
            ReleaseDate = releaseDate;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"Album: {Title}, Artist: {Artist}, Released: {ReleaseDate.ToShortDateString()}, Genre: {Genre.Name}";
        }
    }
}