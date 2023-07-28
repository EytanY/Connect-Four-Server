using Connect_Four_Server.Models.Tables;
using System.Diagnostics.CodeAnalysis;

namespace Connect_Four_Server.Models
{
    public class PlayerEqualityComparerByName : IEqualityComparer<PlayersTbl>
    {
        public bool Equals(PlayersTbl? x, PlayersTbl? y)
        {
            if (x == null ||  y == null) return false;
            return (x.FirstName + x.LastName).Equals(y.FirstName + y.LastName);
        }

        public int GetHashCode([DisallowNull] PlayersTbl obj)
        {
            return (obj.FirstName + obj.LastName).GetHashCode();
        }
    }
}
