using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Connect_Four_Server.Data;
using Connect_Four_Server.Models.Tables;
using Microsoft.CodeAnalysis.Scripting;
using System.Drawing;
using Connect_Four_Server.Models;
using System.Data;
using System.Globalization;

namespace Connect_Four_Server.Pages.Players
{
    public class CreateModel : PageModel
    {
        private readonly Connect_Four_Server.Data.Connect_Four_ServerContext _context;
 

        public CreateModel(Connect_Four_Server.Data.Connect_Four_ServerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            SetCountriesTypes(eCountry.ISRAEL);
            return Page();
        }

        [BindProperty]
        public PlayersTbl PlayersTbl { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string Countries)
        {
          if (!ModelState.IsValid || _context.PlayersTbl == null || PlayersTbl == null)
            {
                return RedirectToPage("./Create");
            }
            try
            {

                    //if id is unique create new player
                    PlayersTbl.Counrty = (int)(eCountry)Enum.Parse(typeof(eCountry), Countries);
                    PlayersTbl.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(PlayersTbl.FirstName);
                    PlayersTbl.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(PlayersTbl.LastName);
                    _context.PlayersTbl.Add(PlayersTbl);
                    await _context.SaveChangesAsync();

                
            }
            catch (Exception)
            {
                string errorMsg = "Please enter unique ID";
                return Content(errorMsg);
            }
            
   
            return RedirectToPage("./Index");
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
