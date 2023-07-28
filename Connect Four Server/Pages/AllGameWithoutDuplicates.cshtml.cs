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

namespace Connect_Four_Server.Pages
{
    public class AllGameWithoutDuplicatesModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public AllGameWithoutDuplicatesModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        public IList<GamesTbl> GamesTbl { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GamesTbl != null)
            {

                // all games from the database
                var allGames = await _context.GamesTbl.ToListAsync();

                // Use the GameEqualityComparer to get distinct games based on player ID
                GamesTbl = allGames.Distinct(new GameEqualityComparer()).ToList();
            }
        }

       
    }
}
