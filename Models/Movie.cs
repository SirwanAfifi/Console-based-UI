using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NStack;
using Terminal.Gui;

namespace TestingGuiCS.Models
{
    public class Movie
    {
        public double Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public MpaaRating MpaaRating { get; set; }
        public string Genre { get; set; }
        public double Rating { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} | ReleaseDate: {ReleaseDate:dd/mm/yyyy} | MpaaRating: {MpaaRating} | Genre: {Genre} | Rating: {Rating}";
        }
    }

    public enum MpaaRating
    {
        G = 1,
        PG = 2,
        PG13 = 3,
        R = 4
    }

    public static class MovieDataSource
    {
        public static Movie GetOne(long id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }
        
        public static IEnumerable<Movie> GetList()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "The Amazing Spider-Man", ReleaseDate = new DateTime(2012, 07, 03), MpaaRating = MpaaRating.PG13, Genre = "Adventure", Rating = 7 },
                new Movie { Id = 2, Name = "Beauty and the Beast", ReleaseDate = new DateTime(2017, 03, 17), MpaaRating = MpaaRating.PG13, Genre = "Family", Rating = 7.8 },
                new Movie { Id = 3, Name = "The Secret Life of Pets", ReleaseDate = new DateTime(2016, 07, 08), MpaaRating = MpaaRating.G, Genre = "Adventure", Rating = 6.6 },
                new Movie { Id = 4, Name = "The Jungle Book", ReleaseDate = new DateTime(2016, 04, 15), MpaaRating = MpaaRating.PG, Genre = "Fantasy", Rating = 7.5 },
                new Movie { Id = 5, Name = "Split", ReleaseDate = new DateTime(2017, 01, 20), MpaaRating = MpaaRating.PG13, Genre = "Horror", Rating = 7.4 },
                new Movie { Id = 6, Name = "The Mummy", ReleaseDate = new DateTime(2017, 06, 09), MpaaRating = MpaaRating.R, Genre = "Action", Rating = 6.7 },
            };
        }

        public static IEnumerable<Movie> GetList(bool forKidsOnly, double minimumRating)
        {
            return GetList().Where(x => (x.MpaaRating <= MpaaRating.PG || !forKidsOnly)
            && x.Rating >= minimumRating).ToList();
        }
        
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}