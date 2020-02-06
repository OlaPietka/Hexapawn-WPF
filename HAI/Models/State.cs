using System.Collections.Generic;

namespace HAI.Models
{
    public class State
    {
        public Board Board;
        public List<Move> PossibleMoves;

        public State(State state)
        {
            Board = new Board(state.Board);
            PossibleMoves = new List<Move>(state.PossibleMoves);
        }

        public State(Board board, List<Move> possibleMoves)
        {
            Board = board;
            PossibleMoves = possibleMoves;
        }

        public static bool operator ==(State obj1, State obj2)
        {
            return obj1.Board == obj2.Board;
        }

        public static bool operator !=(State obj1, State obj2)
        {
            return obj1.Board != obj2.Board;
        }
    }
}