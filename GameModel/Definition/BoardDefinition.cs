using System.Collections.Generic;

namespace GameModel.Definition
{
    public class BoardDefinition
    {
        public BoardDefinition(CellDefinition[] cells)
        {
            Cells = cells;
        }

        public CellDefinition[] Cells { get; }
        public Dictionary<string, CellDefinition> CellsByReference { get; } = new Dictionary<string, CellDefinition>();
    }
}
