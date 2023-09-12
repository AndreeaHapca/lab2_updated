﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Negrisan_Mihai_Lab2.Data;
using Negrisan_Mihai_Lab2.Models;

namespace Negrisan_Mihai_Lab2.Pages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Negrisan_Mihai_Lab2.Data.Negrisan_Mihai_Lab2Context _context;

        public IndexModel(Negrisan_Mihai_Lab2.Data.Negrisan_Mihai_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Publisher != null)
            {
                Publisher = await _context.Publisher.ToListAsync();
            }
        }
    }
}
