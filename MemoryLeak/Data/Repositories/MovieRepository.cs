using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MemoryLeak.Data.Repositories
{
    public class MovieRepository
    {
        private readonly MyAppContext _context;

        public MovieRepository(MyAppContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> Load(CancellationToken cancellationToken)
        {
            //List<Movie> movies = new();

            //for (int i = 0; i < 100_000; i++)
            //{
            //    movies.Add(new Movie { Id = 0, Title = $"Movie{i}" });
            //    movies.AddRange(Enumerable.Range(1, 1000).Select((i, x) => new Movie { Id = i, Title = $"{i}movie{i}" }));
            //    await Task.Delay(1, cancellationToken);
            //}

            var movies = await _context.Movies.OrderByDescending(m => m.Title).ToListAsync(cancellationToken).ConfigureAwait(true);
            return movies;
            //await _context.Database.ExecuteSqlRawAsync("WAITFOR DELAY '00:00:10'", cancellationToken);
        }
    }
}
