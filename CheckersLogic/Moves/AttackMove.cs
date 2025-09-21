namespace CheckersLogic
{
    public class AttackMove : Move
    {
        public override MoveType Type => MoveType.Attack;
        public override Position FromPos { get; }
        public override Position ToPos { get; }
        public Position MiddlePos { get; }

        public AttackMove(Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
            MiddlePos = Position.GetMiddleCell(from, to);
        }

        public override void Execute(Board board)
        {
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null;
            board[MiddlePos] = null;
        }
    }
}
