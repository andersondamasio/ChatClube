using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using com.chatclube.Data.Repository.Config;
using com.chatclube.SalaX;

namespace ChatClube.Web.Pages
{
    public class CreateModel : PageModel
    {
        private readonly com.chatclube.Data.Repository.Config.DBContextCoreSQLite _context;

        public CreateModel(com.chatclube.Data.Repository.Config.DBContextCoreSQLite context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Sala Sala { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sala.Add(Sala);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}