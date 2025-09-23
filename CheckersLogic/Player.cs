namespace CheckersLogic
{
    public enum Player
    {
        White,
        Black
    }

    public static class PlayerExtentions
    {
        public static Player Opponent(this Player player)
        {
            return player switch
            {
                Player.White => Player.Black,
                _ => Player.White,
            };
        }
    }
}
