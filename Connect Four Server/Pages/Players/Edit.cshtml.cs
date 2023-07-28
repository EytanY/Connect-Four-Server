using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Connect_Four_Server.Data;
using Connect_Four_Server.Models.Tables;
using Connect_Four_Server.Models;

namespace Connect_Four_Server.Pages.Players
{
    public class EditModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;

        public EditModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlayersTbl PlayersTbl { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlayersTbl == null)
            {
                return NotFound();
            }

            var playerstbl =  await _context.PlayersTbl.FirstOrDefaultAsync(m => m.Id == id);
            if (playerstbl == null)
            {
                return NotFound();
            }
            SetCountriesTypes((eCountry)Enum.ToObject(typeof(eCountry), playerstbl.Counrty));
            PlayersTbl = playerstbl;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string Countries)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            PlayersTbl.Counrty = (int)(eCountry)Enum.Parse(typeof(eCountry), Countries);
            _context.Attach(PlayersTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersTblExists(PlayersTbl.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlayersTblExists(int id)
        {
          return (_context.PlayersTbl?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void SetCountriesTypes(eCountry selectedMovie)
        {

            IEnumerable<eCountry> values =

                              Enum.GetValues(typeof(eCountry))

                              .Cast<eCountry>();

            IEnumerable<SelectListItem> countries =

                from value in values

                select new SelectListItem

                {

                    Text = value.ToString(),

                    Value = value.ToString(),

                    Selected = value == selectedMovie,

                };

            ViewData["Countries"] = countries;

        }
    }
}
