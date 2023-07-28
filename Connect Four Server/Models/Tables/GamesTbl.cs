using System.ComponentModel.DataAnnotations;

namespace Connect_Four_Server.Models.Tables
{
    public class GamesTbl
    {
        [Key]
        [Display(Name = "Game's ID")]
        public int Id { get; set; }
        [Display(Name = "Player's ID")]
        public int PlayerId { get; set; }
        [Display(Name = "Game's Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Game's Winner")]
        public int Winner { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not GamesTbl )
                return false;
            return ((GamesTbl)obj).Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
