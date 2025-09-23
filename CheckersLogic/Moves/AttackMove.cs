namespace CheckersLogic
{
    public class AttackMove : Move
    {
        public override MoveType Type => MoveType.Attack;
        public override Position FromPos { get; }
        public override Position ToPos { get; }
        public List<Position> Captured { get; }

        public AttackMove(Position from, Position to, List<Position> captured)
        {
            FromPos = from;
            ToPos = to;
            Captured = captured;
        }

        public override void Execute(Board board)
        {
            Piece piece = board[FromPos];
            board[FromPos] = null;
            board[ToPos] = piece;

            foreach (Position pos in Captured)
            {
                board[pos] = null;
            }
        }
    }
}
