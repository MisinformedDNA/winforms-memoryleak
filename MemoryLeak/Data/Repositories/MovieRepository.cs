using System.Collections.Generic;
using System.Linq;
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
            var movies = await _context.Movies.OrderByDescending(m => m.Title).ToListAsync(cancellationToken).ConfigureAwait(true);
            return movies;
        }
    }
}
