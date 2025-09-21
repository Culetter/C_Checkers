namespace CheckersLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool CanAttack { get; set; } = false;
        public abstract IEnumerable<Move> GetMoves(Position from, Board board);
    }
}
