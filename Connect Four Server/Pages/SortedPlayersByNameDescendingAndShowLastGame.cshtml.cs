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
    public class SortedPlayersByNameDescendingAndShowLastGameModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public SortedPlayersByNameDescendingAndShowLastGameModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        public IList<PlayersTbl> PlayersTbl { get;set; } = default!;
        public IList<GamesTbl> GamesTbl { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PlayersTbl != null && _context.GamesTbl != null )
            {
                PlayersTbl = await _context.PlayersTbl.OrderByDescending(row=> row.FirstName + " " + row.LastName).ToListAsync();
                GamesTbl = await _context.GamesTbl.OrderByDescending(game=> game.Date).ToListAsync();


                GamesTbl = PlayersTbl.Select(player => new GamesTbl
                {
                    PlayerId = player.Id,
                    Id = GamesTbl.Where(game=> game.PlayerId ==  player.Id).OrderByDescending(game=>game.Date).Select(game=> game.Id).FirstOrDefault(),
                    Date = GamesTbl.Where(game => game.PlayerId == player.Id).OrderByDescending(game => game.Date).Select(game => game.Date).FirstOrDefault(),
                    Winner = GamesTbl.Where(game => game.PlayerId == player.Id).OrderByDescending(game => game.Date).Select(game => game.Winner).FirstOrDefault()
                }).ToList();



            }
        }
    }
}
