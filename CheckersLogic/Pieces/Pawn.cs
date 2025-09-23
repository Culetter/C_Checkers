namespace CheckersLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        private readonly Direction[] forward;

        public Pawn(Player color)
        {
            Color = color;

            if (color == Player.White)
            {
                forward =
                [
                    Direction.NorthEast,
                    Direction.NorthWest
                ];
            }
            else if (color == Player.Black)
            {
                forward =
                [
                    Direction.SouthEast,
                    Direction.SouthWest
                ];
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            return copy;
        }

        private static bool CanMoveTo(Position pos, Board board)
        {
            return Board.IsInside(pos) && board.IsEmpty(pos);
        }

        private bool CanCaptureAt(Position pos, Board board)
        {
            return Board.IsInside(pos) && !board.IsEmpty(pos) && board[pos].Color != Color;
        }


        private IEnumerable<Move> AttackMoves(Position from, Board board)
        {
            foreach (Move move in GetAttackChains(from, from, board, new List<Position>()))
            {
                yield return move;
            }
        }

        private IEnumerable<Move> GetAttackChains(Position originalFrom, Position currentFrom, Board board, List<Position> captured)
        {
            bool hasCaptured = false;
            foreach (Direction dir in forward)
            {
                Position target = currentFrom + dir;
                Position landing = target + dir;
                if (CanCaptureAt(target, board) && CanMoveTo(landing, board))
                {
                    Board tempBoard = board.Copy();
                    tempBoard[target] = null;
                    tempBoard[landing] = tempBoard[currentFrom];
                    tempBoard[currentFrom] = null;
                    List<Position> newCaptured = new List<Position>(captured) { target };

                    foreach (Move chain in GetAttackChains(originalFrom, landing, tempBoard, newCaptured))
                    {
                        hasCaptured = true;
                        yield return chain;
                    }

                    if (!hasCaptured)
                    {
                        yield return new AttackMove(originalFrom, landing, newCaptured);
                    }
                }
            }

            if (!hasCaptured && captured.Count > 0)
            {
                yield return new AttackMove(originalFrom, currentFrom, captured);
            }
        }

        private IEnumerable<Move> NormalMoves(Position from, Board board)
        {
            foreach (Direction dir in forward)
            {
                Position to = from + dir;

                if (CanMoveTo(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            IEnumerable<Move> attackMoves = AttackMoves(from, board);

            if (attackMoves.Any()) return attackMoves;

            return NormalMoves(from, board);
        }
    }
}
