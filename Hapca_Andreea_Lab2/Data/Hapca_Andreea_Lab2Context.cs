using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hapca_Andreea_Lab2.Models;
using HapcaAndreea_Lab2.Models;

namespace Hapca_Andreea_Lab2.Data
{
    public class Hapca_Andreea_Lab2Context : DbContext
    {

        public Hapca_Andreea_Lab2Context (DbContextOptions<Hapca_Andreea_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Hapca_Andreea_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Hapca_Andreea_Lab2.Models.Publisher>? Publisher { get; set; }

        public DbSet<Hapca_Andreea_Lab2.Models.Author>? Authors { get; set; } = default!;

        public DbSet<HapcaAndreea_Lab2.Models.Category>? Category { get; set; }
    }
}
