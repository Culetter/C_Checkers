namespace CheckersLogic
{
    public class AttackPromotion : AttackMove
    {
        public AttackPromotion(Position from, Position to, List<Position> captured) : base(from, to, captured) { }

        public override void Execute(Board board)
        {
            Piece pawn = board[FromPos];
            board[FromPos] = null;

            if (Captured.Count > 0)
                foreach (Position pos in Captured)
                    board[pos] = null;

            Piece promotionPiece = new Queen(pawn.Color);
            board[ToPos] = promotionPiece;
        }
    }
}
