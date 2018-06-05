namespace GameModel.Definition
{
    public class GameDefinition
    {
        public GameDefinition(PlayerDefinition[] players, BoardDefinition[] boards)
        {
            Players = players;
            Boards = boards;
        }

        public PlayerDefinition[] Players { get; }
        public BoardDefinition[] Boards { get; }
    }
}
