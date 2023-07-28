using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Connect_Four_Server.Data;
using Connect_Four_Server.Models.Tables;

namespace Connect_Four_Server.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public DeleteModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        [BindProperty]
      public GamesTbl GamesTbl { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GamesTbl == null)
            {
                return NotFound();
            }

            var gamestbl = await _context.GamesTbl.FirstOrDefaultAsync(m => m.Id == id);

            if (gamestbl == null)
            {
                return NotFound();
            }
            else 
            {
                GamesTbl = gamestbl;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.GamesTbl == null)
            {
                return NotFound();
            }
            var gamestbl = await _context.GamesTbl.FindAsync(id);

            if (gamestbl != null)
            {
                GamesTbl = gamestbl;
                _context.GamesTbl.Remove(GamesTbl);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
