using Chess.Pieces;
using Chess.Commands;
using Chess.Status;

namespace Chess
{
    public class Game
    {
        private Board _board = new Board();
        private View view;

        public BoardView Board { get => _board; }

        private Player white;
        private Player black;

        public Game(View view)
        {
            this.view = view;
            white = new Player(Team.White, _board);
            black = new Player(Team.Black, _board);
        }

        public void Play()
        {
            
            State state = new PlayingState();
            Player currentPlayer = white;
            while (state is PlayingState)
            {
                view.Display(Board);
                view.Alert(state.Message);
                state = ProcessTurn(currentPlayer);

                currentPlayer = GetNextPlayer(currentPlayer);
            }
            view.Display(Board);
            view.Alert(state.Message);
        }

        private Player GetNextPlayer(Player player)
        {
            if (player.Equals(black))
                return white;
            else
                return black;
        }

        private State ProcessTurn(Player player)
        {
            Command command = view.GetCommand(player);
            State state = command.Execute(player);

            while (state is WrongCommandState)
            {
                view.Alert(state.Message);
                command = view.GetCommand(player);
                state = command.Execute(player);
            }
            Judge judge = new Judge(_board);
            Player oppositePlayer = GetNextPlayer(player);
            state = judge.CheckOpposite(oppositePlayer);

            return state;
        }
    }
}
