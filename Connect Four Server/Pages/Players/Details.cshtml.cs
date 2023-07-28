using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Connect_Four_Server.Data;
using Connect_Four_Server.Models.Tables;
using Connect_Four_Server.Models;

namespace Connect_Four_Server.Pages.Players
{
    public class DetailsModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public DetailsModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

      public PlayersTbl PlayersTbl { get; set; } = default!; 
        public string? CounrtyName { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlayersTbl == null)
            {
                return NotFound();
            }

            var playerstbl = await _context.PlayersTbl.FirstOrDefaultAsync(m => m.Id == id);
            if (playerstbl == null)
            {
                return NotFound();
            }
            else 
            {
                CounrtyName = Enum.GetName(typeof(eCountry), playerstbl.Counrty);
                PlayersTbl = playerstbl;
            }
            return Page();
        }
    }
}
