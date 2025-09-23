using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersLogic
{
    public class NormalPromotion : NormalMove
    {   
        public NormalPromotion(Position from, Position to) : base(from, to) { }

        public override void Execute(Board board)
        {
            Piece pawn = board[FromPos];
            board[FromPos] = null;

            Piece promotionPiece = new Queen(pawn.Color);
            board[ToPos] = promotionPiece;
        }
    }
}
