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
    public class DetailsModel : PageModel
    {
        private readonly com.chatclube.Data.Repository.Config.DBContextCoreSQLite _context;

        public DetailsModel(com.chatclube.Data.Repository.Config.DBContextCoreSQLite context)
        {
            _context = context;
        }

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
    }
}
