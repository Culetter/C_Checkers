using CheckersLogic;

namespace CheckersLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        protected override Direction[] Forward { get; }

        public Pawn(Player color)
        {
            Color = color;

            if (color == Player.White)
            {
                Forward =
                [
                    Direction.NorthEast,
                    Direction.NorthWest
                ];
            }
            else if (color == Player.Black)
            {
                Forward =
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
    }
}
