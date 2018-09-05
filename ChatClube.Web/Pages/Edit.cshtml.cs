using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using com.chatclube.Data.Repository.Config;
using com.chatclube.SalaX;

namespace ChatClube.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly com.chatclube.Data.Repository.Config.DBContextCoreSQLite _context;

        public EditModel(com.chatclube.Data.Repository.Config.DBContextCoreSQLite context)
        {
            _context = context;
        }

        [BindProperty]
        public Sala Sala { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sala = await _context.Sala.FirstOrDefaultAsync(m => m.IDSala == id);

            if (Sala == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Sala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaExists(Sala.IDSala))
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

        private bool SalaExists(int id)
        {
            return _context.Sala.Any(e => e.IDSala == id);
        }
    }
}
