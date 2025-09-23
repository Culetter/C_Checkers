namespace CheckersLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public abstract Piece Copy();
        public abstract IEnumerable<Move> GetMoves(Position from, Board board);
    }
}
