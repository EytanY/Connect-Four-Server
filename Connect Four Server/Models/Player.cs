using System.Diagnostics.Metrics;

namespace Connect_Four_Server.Models
{
    public class Player
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; } 

        public int GamesNum { get; set; }

        public string Phone { get; set; }

        public eCountry Counrty { get; set; }

        public Player(string firstName, string lastName, int id, eCountry counrty, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            Counrty = counrty;
            Phone = phone;
            GamesNum = 0;
        }
        public Player(string firstName, string lastName, int id, eCountry counrty, string phone, int gameNum)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            Counrty = counrty;
            Phone = phone;
            GamesNum = gameNum;
        }

        public int AddGame()
        {
            return GamesNum++;
        }

    }
}

