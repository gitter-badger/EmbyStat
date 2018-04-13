﻿using System.Collections.Generic;
using System.Linq;
using EmbyStat.Common.Models;
using EmbyStat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EmbyStat.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public void Add(Movie movie)
        {
            using (var context = new ApplicationDbContext())
            {
                var peopleToDelete = new List<string>();
                foreach (var person in movie.ExtraPersons)
                {
                    var temp = context.People.AsNoTracking().SingleOrDefault(x => x.Id == person.PersonId);
                    if (temp == null)
                    {
                        Log.Warning($"We couldn't find the person with Id {person.PersonId} for movie ({movie.Id}) {movie.Name} in our database. This is because Emby didn't return the actor when we queried the people for the parent id. As a fix we will remove the genre from the movie now.");
                        peopleToDelete.Add(person.PersonId);
                    }
                }
                peopleToDelete.ForEach(x => movie.ExtraPersons.Remove(movie.ExtraPersons.SingleOrDefault(y => y.PersonId == x)));

                var genresToDelete = new List<string>();
                foreach (var genre in movie.MediaGenres)
                {
                    var temp = context.Genres.AsNoTracking().SingleOrDefault(x => x.Id == genre.GenreId);
                    if (temp == null)
                    {
                        Log.Warning($"We couldn't find the genre with Id {genre.GenreId} for movie ({movie.Id}) {movie.Name} in our database. This is because Emby didn't return the genre when we queried the genres for the parent id. As a fix we will remove the genre from the movie now.");
                        genresToDelete.Add(genre.GenreId);
                    }
                }
                genresToDelete.ForEach(x => movie.MediaGenres.Remove(movie.MediaGenres.SingleOrDefault(y => y.GenreId == x)));

                context.Movies.Add(movie);
                context.SaveChanges();
            }
        }

        public void RemoveMovies()
        {
            using (var context = new ApplicationDbContext())
            {
                var movieIds = context.Movies.Select(x => x.Id).ToList();

                var personsToRemove = context.ExtraPersons.Where(x => movieIds.Any(y => y == x.ExtraId)).ToList();
                context.ExtraPersons.RemoveRange(personsToRemove);

                var genresToRemove = context.MediaGenres.Where(x => movieIds.Any(y => y == x.MediaId)).ToList();
                context.MediaGenres.RemoveRange(genresToRemove);

                context.Movies.RemoveRange(context.Movies);
                context.SaveChanges();
            }
        }
    }
}