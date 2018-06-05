using GameModel.Definition;
using RulesParser;
using System.Collections.Generic;

namespace GameParser.Builders
{
    public class BoardDefinitionBuilder : IBuilder<BoardDefinition>
    {
        public int Width { get; private set; } = 0;
        public int Height { get; private set; } = 0;
        public bool HasSpecifiedDimensions { get; private set; } = false;
        public void SetGridDimensions(int width, int height)
        {
            Width = width;
            Height = height;
            HasSpecifiedDimensions = true;
        }

        public bool AllowMultipleOccupancy { get; set; } = true;

        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public int BorderWidth { get; set; } = 0;

        public bool CanBuild()
        {
            return HasSpecifiedDimensions;
        }

        public BoardDefinition Build()
        {
            var cells = new List<CellDefinition>();
            for (int i = Width * Height; i > 0; i--)
                cells.Add(new CellDefinition(BackgroundColor));

            return new BoardDefinition(cells.ToArray(), AllowMultipleOccupancy, BorderWidth, BorderColor);
        }
    }
}
