using CheckersLogic;

namespace CheckersLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;

        private int noCaptureOrPawnMoves = 0;

        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];

            if (!AreAttackMoves(CurrentPlayer))
            {
                return piece.GetMoves(pos, Board);
            }

            return piece.GetMoves(pos, Board).Where(move => move.Type == MoveType.Attack);
        }

        public void MakeMove(Move move)
        {
            bool captureOrPawn = move.Execute(Board);

            if (captureOrPawn)
            {
                noCaptureOrPawnMoves = 0;
            }
            else
            {
                noCaptureOrPawnMoves++;
            }

            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver();
        }

        private bool AreAttackMoves(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });

            return moveCandidates.Any(move => move.Type == MoveType.Attack);
        }

        private void CheckForGameOver()
        {
            if (!Board.PiecePositionsFor(CurrentPlayer).Any())
            {
                Result = Result.Win(CurrentPlayer.Opponent());
            }
            else if (Board.InsufficientMaterial())
            {
                Result = Result.Draw(EndReason.InsufficientMaterial);
            }
            else if (FortyMoveRule())
            {
                Result = Result.Draw(EndReason.FortyMoveRule);
            }
        }

        public bool IsGameOver()
        {
            return Result != null;
        }

        private bool FortyMoveRule()
        {
            int fullMoves = noCaptureOrPawnMoves / 2;
            return fullMoves == 40;
        }
    }
}
