using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Negrisan_Mihai_Lab2.Models;

namespace Negrisan_Mihai_Lab2.Data
{
    public class Negrisan_Mihai_Lab2Context : DbContext
    {
        public Negrisan_Mihai_Lab2Context (DbContextOptions<Negrisan_Mihai_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Negrisan_Mihai_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Negrisan_Mihai_Lab2.Models.Publisher>? Publisher { get; set; }

        public DbSet<Negrisan_Mihai_Lab2.Models.Author>? Authors { get; set; } = default!;
    }
}
