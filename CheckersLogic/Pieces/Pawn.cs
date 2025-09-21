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

        private static bool CanMoveTo(Position pos, Board board)
        {
            return Board.IsInside(pos) && board.IsEmpty(pos);
        }

        private bool CanCaptureAt(Position pos, Board board, Direction dir)
        {
            return Board.IsInside(pos) && !board.IsEmpty(pos) && board[pos].Color != Color;
        }

        private IEnumerable<Move> AttackMoves(Position from, Board board)
        {
            foreach (Direction dir in forward)
            {
                Position to = from + dir;
                Position landingPos = to + dir;

                if (CanCaptureAt(to, board, dir) && CanMoveTo(landingPos, board))
                {
                    yield return new AttackMove(from, landingPos);
                }
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
