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
    public class IndexModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public IndexModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        public IList<GamesTbl> GamesTbl { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GamesTbl != null)
            {
                GamesTbl = await _context.GamesTbl.ToListAsync();
            }
        }
    }
}
