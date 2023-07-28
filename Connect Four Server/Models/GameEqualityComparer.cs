using Connect_Four_Server.Models.Tables;

namespace Connect_Four_Server.Models
{
    public class GameEqualityComparer : IEqualityComparer<GamesTbl>
    {
        public bool Equals(GamesTbl? x, GamesTbl? y)
        {
            if (x == null || y == null) return false;
            return x.PlayerId == y.PlayerId;
        }

        public int GetHashCode(GamesTbl obj)
        {
            return obj.PlayerId.GetHashCode();
        }
    }
}
