using System.Collections.Generic;

namespace GameModel.Definition
{
    public class Board
    {
        public Board(Cell[] cells)
        {
            Cells = cells;
        }

        public Cell[] Cells { get; }
        public Dictionary<string, Cell> CellsByReference { get; } = new Dictionary<string, Cell>();
    }
}
