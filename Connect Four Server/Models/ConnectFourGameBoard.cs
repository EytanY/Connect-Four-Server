using Newtonsoft.Json;

namespace Connect_Four_Server.Models
{
    public enum PlayerTool 
    {
        RED,
        YELLOW,
        NONE,
        TIE
    }
    public class ConnectFourGameBoard
    {
        public const int ROWS_NUM = 6;
        public const int COLUMNS_NUM = 7;
        public const int CONNECT_NUM = 4;

        [JsonProperty]
        public int CountUpdate { get; set; }
        [JsonProperty]
        public PlayerTool[,] Board { get; set; }


        public ConnectFourGameBoard()
        {
            /***
             * Initialize game board by PlayerTool.NONE 
             * ***/

            Board = new PlayerTool[ROWS_NUM, COLUMNS_NUM];
            for (int row = 0; row < ROWS_NUM; row++)
            {
                for (int col = 0; col < COLUMNS_NUM; col++)
                {
                    Board[row, col] = PlayerTool.NONE;
                }
            }
            CountUpdate = 0;
        }


        public bool IsValidIndex(int row, int col)
        {
            // check if row and col is valid index
            return (row >= 0 && row < ROWS_NUM && col >= 0 && col < COLUMNS_NUM);
        }
        public bool IsIndexIsEmpty(int row, int col)
        {
            // check if the index is valid and if empty = PlayerTool.NONE
            return IsValidIndex(row, col) && Board[row, col] == PlayerTool.NONE;
        }


        public bool IsColumnAvailable(int col)
        {
            return IsIndexIsEmpty(0, col);
        }

        public int GetNextAvailableRowByCol(int col)
        {
            for (int row = ROWS_NUM - 1; row >= 0; row--)
            {
                if (IsIndexIsEmpty(row, col))
                    return row;
            }
            return -1;
        }

        public Tuple<bool, PlayerTool> UpdateBoard(int row, int col, PlayerTool playerTool)
        {
            // ***
            // The function update the game board and retrun Tuple<bool, PlayerTool>
            // If the update is successed the bool result in the Tuple will be true
            // If we have a winner in the game after the update the function will return the Player Tool, else it is return playerToo.None
            //

            if (playerTool == PlayerTool.NONE)
                return Tuple.Create(false, PlayerTool.NONE);

            if (IsIndexIsEmpty(row, col))
            {
                Board[row, col] = playerTool;
                CountUpdate++;
                if (IsWinner(row, col, playerTool))
                    return Tuple.Create(true, playerTool);
                else if (CountUpdate == ROWS_NUM * COLUMNS_NUM)
                    return Tuple.Create(true, PlayerTool.TIE);
                else
                    return Tuple.Create(true, PlayerTool.NONE);
            }

            return Tuple.Create(false, PlayerTool.NONE);
        }

        public bool IsWinner(int row, int col, PlayerTool playerTool)
        {
            //The function will return if the player is a winner

            // check winner in rows
            if (IsWinnerInRows(row, col, playerTool))
                return true;
            // check winner in cols
            if (IsWinnerInCols(row, col, playerTool))
                return true;
            // check winner in diagonals
            if (IsWinnerInDiagonals(row, col, playerTool))
                return true;
            return false;

        }

        private bool IsWinnerInDiagonals(int row, int col, PlayerTool playerTool)
        {
            // check in 4 possible diagonals
            int counterToUpLeft = 0;
            int counterToUpRight = 0;
            int counterToDownLeft = 0;
            int counterToDownRight = 0;
            for (int i = 0; i < CONNECT_NUM; i++)
            {
                // check from index to up left
                if (IsValidIndex(row - i, col - i) && Board[row - i, col - i] == playerTool)
                    counterToUpLeft++;
                // check from index to up right
                if (IsValidIndex(row - i, col + i) && Board[row - i, col + i] == playerTool)
                    counterToUpRight++;
                // check from index to down left
                if (IsValidIndex(row + i, col - i) && Board[row + i, col - i] == playerTool)
                    counterToDownLeft++;
                // check from index to down right
                if (IsValidIndex(row + i, col + i) && Board[row + i, col + i] == playerTool)
                    counterToDownRight++;
            }

            return counterToUpLeft == CONNECT_NUM || counterToUpRight == CONNECT_NUM || counterToDownLeft == CONNECT_NUM || counterToDownRight == CONNECT_NUM;
        }

        private bool IsWinnerInCols(int row, int col, PlayerTool playerTool)
        {
            int counterToUp = 0;
            int counterToDown = 0;
            for (int i = 0; i < CONNECT_NUM; i++)
            {
                // check from index to down
                if (IsValidIndex(row + i, col) && Board[row + i, col] == playerTool)
                    counterToUp += 1;
                // check from index to up
                if (IsValidIndex(row - i, col) && Board[row - i, col] == playerTool)
                    counterToDown += 1;
            }
            return counterToUp == CONNECT_NUM || counterToDown == CONNECT_NUM;
        }

        private bool IsWinnerInRows(int row, int col, PlayerTool playerTool)
        {
            int counterToLeft = 0;
            int counterToRight = 0;
            for (int i = 0; i < CONNECT_NUM; i++)
            {
                // check from index to right
                if (IsValidIndex(row, col + i) && Board[row, col + i] == playerTool)
                    counterToRight += 1;
                // check from indec to left
                if (IsValidIndex(row, col - i) && Board[row, col - i] == playerTool)
                    counterToLeft += 1;
            }
            return counterToLeft == CONNECT_NUM || counterToRight == CONNECT_NUM;
        }

        public Move GetNextMove()
        {
            int row;

            for(int col=0; col < COLUMNS_NUM;  col++)
            {
                row = GetNextAvailableRowByCol(col);
                if (row != -1)
                {
                    return new Move(row, col, 0);
                }
            }
            return new Move(-1, -1, PlayerTool.NONE);
        }
    }
}
