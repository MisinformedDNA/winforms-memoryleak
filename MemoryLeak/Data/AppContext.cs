using MemoryLeak;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLeak.Data
{
    public partial class MyAppContext : DbContext
    {
        [ActivatorUtilitiesConstructor]
        //public NbiContext(DbContextOptions<NbiContext> options, IUserProvider userProvider)
        //    : base(options)
        //{
        //    _userFacilities = userProvider.GetFacilityIds();
        //}

        public MyAppContext(DbContextOptions<MyAppContext> options, UserProvider userProvider)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = default!;
    }
}
