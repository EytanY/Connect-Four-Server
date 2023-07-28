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
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Connect_Four_Server.Pages
{
    public class AllGamesByPlayerNameModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;
        private static string ALL = "All";
    
        public AllGamesByPlayerNameModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
            
        }

        public IList<GamesTbl> GamesTbl { get;set; } = default!;
        public IList<PlayersTbl> PlayersTbl { get; set; } = default!;
        
        
        public async Task OnGetAsync(string? PlayersName)
        {
            if (_context.GamesTbl == null || _context.PlayersTbl == null)
                return;

            PlayersTbl = await _context.PlayersTbl.ToListAsync();
            SetPlayersName(ALL);
            if (PlayersName == null || PlayersName.Equals(ALL))
            {
                GamesTbl = await _context.GamesTbl.ToListAsync(); 
            }
            else
            {
                var playersId = PlayersTbl.Where(player => (player.FirstName + " " + player.LastName).Equals(PlayersName)).Select(player => player.Id).ToList();
                GamesTbl = await _context.GamesTbl.Where(game=> playersId.Contains(game.PlayerId)).ToListAsync();
            }
        }
        
        public async void NameChosen(string? PlayersName)
        {
            if (PlayersName == null || _context.GamesTbl == null || _context.PlayersTbl == null)
            {
                return;
            }
            var playersId = PlayersTbl.Where(player=>(player.FirstName +" " + player.LastName).Equals(PlayersName)).Select(player=>player.Id).ToList();

            GamesTbl = await _context.GamesTbl.Where(game=>playersId.Contains(game.PlayerId)).ToListAsync();
            

        }



        //public iactionresult onpost([fromform] string? playersname)
        //{
        //    if (!modelstate.isvalid || _context.playerstbl == null || playerstbl == null)
        //    {
        //        return page();
        //    }
        //    try
        //    {
        //        return content($"<p>games of player  {playersname} </p>", "text/html");
        //    }
        //    catch (exception)
        //    {

        //        return page();
        //    }

        //}

        private void SetPlayersName(string selected)
        {
            var playersDistinct = PlayersTbl.Distinct(new PlayerEqualityComparerByName()).OrderBy(p=>(p.FirstName + p.LastName)).ToList();

            List<string> values = new List<string>
            {
                ALL
            };
            foreach (var player in playersDistinct)
            {
                values.Add(player.FirstName + " " + player.LastName);
            }

            IEnumerable<SelectListItem> names =

                from value in values

                select new SelectListItem

                {

                    Text = value.ToString(),

                    Value = value.ToString(),

                    

                };
                ViewData["PlayersName"] = names;

        }
    }
}
