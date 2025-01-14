namespace QueensGame
{
    public class Game
    {
        public Board Board { get; private set; }
        public object ForwardCheckingEnabled { get; internal set; }

        public int QueenCount => Board.QueenCount;
        public Game()
        {
            Board = new Board();
        }

        // plasare regina
        public void HandleMouseClick(int x, int y)
        {
            Board.HandleMouseClick(x, y);  // se apelează logica din Board
        }


        // resetare joc
        public void ResetGame()
        {
            Board.ResetGame();
        }

        public string CheckGameStatus(int clickCount)
        {
            if (clickCount == 4 && QueenCount == 4)
            {
                return "Ai câștigat!";
            }
            else if (clickCount == 4 && QueenCount < 4)
            {
                return "Ai pierdut!";
            }
            return null; 
        }
    }
}
