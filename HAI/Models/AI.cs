using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAI.Models
{
	class AI
	{
		public List<(State State, Move Move)> _currentGame = new List<(State state, Move move)>();
		/// <summary>
		/// Used so that we can know what States the AI has already visited 
		/// (compared with currentGame so that it doesn't update)
		/// </summary>
		public List<State> _seenStates = new List<State>();

        private Random _rnd = new Random();

		/// <summary>
		/// Generates all possible moves for AI in given State
		/// </summary>
		/// <param name="currentState"></param>
		/// <returns>All possible moves by AI in given State</returns>
		public List<Move> GeneratePossibleMoves(State currentState)
		{
            var possibleMoves = new List<Move>();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					var point = new Point(i, j);
                    if (currentState.Board.GetField(point) is Field.AI)
                        possibleMoves.AddRange(currentState.Board.PossibleMoves(point, Field.AI));
				}
			}

            return possibleMoves;
		}

		public void Update()
		{
            var state = _seenStates.Find(s => s.Board == _currentGame.Last().State.Board);

            state.PossibleMoves.Remove(_currentGame.Last().Move);

            if (state.PossibleMoves.Count < 1)
                _seenStates.Remove(state); //Usun siebie

            StatsData.Add();
        }

        public Move GetMove(State currentState)
		{
            // Adds a new state thats never been seen before and generates possible moves for AI
            currentState.PossibleMoves = GeneratePossibleMoves(currentState);

            if (!_seenStates.Any(s => currentState.Board == s.Board))
            {
                _seenStates.Add(new State(currentState));
                _currentGame.Add((new State(currentState), new Move()));
            }
            else
                _currentGame.Add((_seenStates.Find(s => s.Board == currentState.Board), new Move()));


            // Get random move from possibleMoves
            var (state, _) = _currentGame.Last();
            var move = state.PossibleMoves.ElementAt(_rnd.Next(0, state.PossibleMoves.Count - 1));

            _currentGame.RemoveAt(_currentGame.Count - 1);
            _currentGame.Add((new State(state), move));

            return move;
		}
	}
}
