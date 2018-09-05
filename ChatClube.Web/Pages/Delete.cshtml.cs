using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using com.chatclube.Data.Repository.Config;
using com.chatclube.SalaX;

namespace ChatClube.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly com.chatclube.Data.Repository.Config.DBContextCoreSQLite _context;

        public DeleteModel(com.chatclube.Data.Repository.Config.DBContextCoreSQLite context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sala = await _context.Sala.FindAsync(id);

            if (Sala != null)
            {
                _context.Sala.Remove(Sala);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
