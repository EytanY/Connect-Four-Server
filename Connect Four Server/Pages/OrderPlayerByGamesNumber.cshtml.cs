using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Connect_Four_Server.Data;
using Connect_Four_Server.Models.Tables;

namespace Connect_Four_Server.Pages
{
    public class OrderPlayerByGamesNumberModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public OrderPlayerByGamesNumberModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        public IList<PlayersTbl> PlayersTbl { get;set; } = default!;
        public IEnumerable<IGrouping<int, PlayersTbl>> GroupedPlayers { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.PlayersTbl != null)
            {

                PlayersTbl = await _context.PlayersTbl.ToListAsync();
                GroupedPlayers = from player in PlayersTbl
                                 group player by player.GamesNum
                                 ;
                GroupedPlayers= GroupedPlayers.OrderBy(group=> group.Key);

            }
        }
    }
}
