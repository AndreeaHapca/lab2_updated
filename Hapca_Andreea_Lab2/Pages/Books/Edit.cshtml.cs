using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hapca_Andreea_Lab2.Data;
using Hapca_Andreea_Lab2.Models;
using HapcaAndreea_Lab2.Models;
using Hapca_Andreea_Lab2.Migrations;

namespace Hapca_Andreea_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Hapca_Andreea_Lab2.Data.Hapca_Andreea_Lab2Context _context;

        public EditModel(Hapca_Andreea_Lab2.Data.Hapca_Andreea_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;
        public Models.Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book =  await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
            var author = await _context.Authors.FirstOrDefaultAsync(m => m.ID == id);
            if (book == null || author == null)
            {
                return NotFound();
            }
            Book = book;
            Author = author;

            PopulateAssignedCategoryData(_context, Book);


            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Models.Author>(), "ID", "FirstName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookToUpdate = await _context.Book
                                .Include(i => i.Publisher)
                                .Include(i => i.BookCategories)
                                .ThenInclude(i => i.Category)
                                .FirstOrDefaultAsync(s => s.ID == id);
            var authorToUpdate = await _context.Authors
                                .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null || authorToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                            bookToUpdate,
                             "Book",
                             i => i.Title, i => i.Author,
                                 i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            if (await TryUpdateModelAsync<Models.Author>(
                            authorToUpdate,
                             "Author",
                             i => i.FirstName, i => i.LastName))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }


            if (!ModelState.IsValid)
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                PopulateAssignedCategoryData(_context, bookToUpdate);
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
