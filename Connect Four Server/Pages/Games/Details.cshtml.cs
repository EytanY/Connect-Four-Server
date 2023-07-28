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
    public class DetailsModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public DetailsModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        public GamesTbl GamesTbl { get; set; } = default!;
        public PlayersTbl PlayerTbl { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GamesTbl == null || _context.PlayersTbl == null)
            {
                return NotFound();
            }

            var game = await _context.GamesTbl.FirstOrDefaultAsync(m => m.Id == id);
            var player = await _context.PlayersTbl.FirstOrDefaultAsync(p => p.Id == game.PlayerId);
            if (game == null || player == null)
            {
                return NotFound();
            }
            else 
            {
                GamesTbl = game;
                PlayerTbl = player;
            }
            return Page();
        }
    }
}
