using System.Collections.Generic;

namespace HAI.Models
{
    public enum Field
    {
        Empty,
        AI,
        Player
    }

    public class Board
    {
        private Field[,] _board;

        public Board()
        {
            _board = InitBoard();
        }

        public Board(Board board)
        {
            _board = (Field[,])board._board.Clone();
        }

        private static Field[,] InitBoard()
        {
            return new Field[3, 3]
            {
                {Field.AI, Field.AI, Field.AI },
                {Field.Empty, Field.Empty, Field.Empty },
                {Field.Player, Field.Player, Field.Player }
            };
        }

        private void SetField(Point point, Field field)
        {
            _board[point.Y, point.X] = field;
        }

        private bool IsInRange(Point point)
        => (point.X >= 0 && point.X < 3 && point.Y >= 0 && point.Y < 3);

        public bool IsPlayer(Point point)
            => GetField(point) is Field.Player;

        public Field GetField(Point point)
            => _board[point.Y, point.X];

        public List<Move> PossibleMoves(Point point, Field filed)
        {
            var moves = new List<Move>();
            var offset = filed is Field.Player ? -1 : 1;
            var newPoint = new Point(point.X, point.Y + offset);

            if (IsInRange(newPoint) && GetField(newPoint) is Field.Empty)
                moves.Add(new Move(point, newPoint));
            
            newPoint = new Point(point.X+1, point.Y + offset);
            if (IsInRange(newPoint) && GetField(newPoint) == (filed is Field.Player ? Field.AI : Field.Player))
                moves.Add(new Move(point, newPoint));

            newPoint = new Point(point.X-1, point.Y + offset);
            if (IsInRange(newPoint) && GetField(newPoint) == (filed is Field.Player ? Field.AI : Field.Player))
                moves.Add(new Move(point, newPoint));

            return moves;
        }

        public bool IsWin(Field field)
        {
            var allPossibleMoves = new List<List<Move>>();
            var winRow = field is Field.Player ? 0 : 2;
            var opponent = field is Field.Player ? Field.AI : Field.Player;

            for (var i = 0; i < 3; i++)
            {
                if (_board[winRow, i] == field)
                    return true;

                for (var j = 0; j < 3; j++)
                    if(_board[i,j] == opponent)
                        allPossibleMoves.Add(PossibleMoves(new Point(j, i), opponent));
            }

            if (allPossibleMoves.TrueForAll(m=>m.Count ==0))
                return true;

            return false;
        }

        public void MovePawn(Move move)
        {
            SetField(move.To, GetField(move.From));
            SetField(move.From, Field.Empty);
        }

        public static bool operator ==(Board obj1, Board obj2)
        {
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    if (obj1._board[i, j] != obj2._board[i, j])
                        return false;
            return true;
        }

        public static bool operator !=(Board obj1, Board obj2)
        {
            var count = 0;
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    if (obj1._board[i, j] == obj2._board[i, j])
                        count++;

            if (count == 9)
                return false;

            return true;
        }
    }
}