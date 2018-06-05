using System.Collections.Generic;

namespace GameModel.Definition
{
    public class BoardDefinition
    {
        public BoardDefinition(CellDefinition[] cells, bool allowMultipleOccupancy, int borderWidth, string borderColor)
        {
            Cells = cells;
            AllowMultipleOccupancy = allowMultipleOccupancy;
            BorderWidth = borderWidth;
            BorderColor = borderColor;
        }

        public CellDefinition[] Cells { get; }
        public Dictionary<string, CellDefinition> CellsByReference { get; } = new Dictionary<string, CellDefinition>();

        public bool AllowMultipleOccupancy { get; }
        public int BorderWidth { get; }
        public string BorderColor { get; }
    }
}
