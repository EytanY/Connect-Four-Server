using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Connect_Four_Server.Models.Tables
{

    public class PlayersTbl
    {
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Plaese enter at least 2 characters")]
        public string FirstName { get; set; } = default!;
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Plaese enter at least 2 characters")]
        public string LastName { get; set; } = default!;
        [Range(1, 1000, ErrorMessage = "You must enter unique ID in range 1-1000")]
        public int Id { get; set; } = default!;
        [Display(Name = "Number of Games")]
        public int GamesNum { get; set; } = default!;
        [Phone]
        [RegularExpression(@"^\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage ="Please enter valid phone number")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = default!;

        public int Counrty { get; set; } = default!;

        public static implicit operator PlayersTbl(List<PlayersTbl> v)
        {
            throw new NotImplementedException();
        }
    }
}
