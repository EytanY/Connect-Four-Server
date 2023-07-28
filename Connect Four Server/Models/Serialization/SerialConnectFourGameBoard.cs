using Newtonsoft.Json;

namespace Connect_Four_Server.Models.Serialization
{
    public class SerialConnectFourGameBoard
    {


        [JsonProperty]
        private int CountUpdate { get; set; }
        [JsonProperty]
        public int[] Board { get; set; }
        [JsonConstructor]
        public SerialConnectFourGameBoard(int countUpdate, int[] board)
        {
            CountUpdate = countUpdate;
            Board = board;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SerialConnectFourGameBoard()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public SerialConnectFourGameBoard(ConnectFourGameBoard connectFourGameBoard)
        {
            CountUpdate = connectFourGameBoard.CountUpdate;
            Board = new int[ConnectFourGameBoard.ROWS_NUM * ConnectFourGameBoard.COLUMNS_NUM];
            for(int i = 0; i < ConnectFourGameBoard.ROWS_NUM; i++)
            {
                for(int j = 0; j < ConnectFourGameBoard.COLUMNS_NUM; j++)
                {
                    Board[i * ConnectFourGameBoard.COLUMNS_NUM + j] = (int)connectFourGameBoard.Board[i, j];
                }
            }  
        }

        public ConnectFourGameBoard GetConnectFourGameBoard()
        {
            ConnectFourGameBoard connectFourGameBoard = new ConnectFourGameBoard();
            connectFourGameBoard.CountUpdate = CountUpdate;
            for (int i = 0; i < ConnectFourGameBoard.ROWS_NUM; i++)
            {
                for (int j = 0; j < ConnectFourGameBoard.COLUMNS_NUM; j++)
                {
                    connectFourGameBoard.Board[i, j] = (PlayerTool)Enum.ToObject(typeof(PlayerTool), Board[i * ConnectFourGameBoard.COLUMNS_NUM + j]);
                }
            }
            return connectFourGameBoard;

        }

    }
}
