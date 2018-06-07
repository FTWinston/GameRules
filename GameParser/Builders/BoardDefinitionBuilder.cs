using GameModel.Definition;
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

        public string[] BackgroundColors { get; set; } = new[] { "grey" };
        public string BorderColor { get; set; } = null;
        public int BorderWidth { get; set; } = 0;

        public bool CanBuild()
        {
            return HasSpecifiedDimensions;
        }

        public BoardDefinition Build()
        {
            var cells = new List<CellDefinition>();
            int iColor = 0;

            for (int row = 0; row < Height; row++)
                for (int col = 0; col < Width; col++)
                {
                    var color = BackgroundColors[iColor++];
                    cells.Add(new CellDefinition(color));

                    if (iColor >= BackgroundColors.Length)
                        iColor = 0;
                }

            return new BoardDefinition(cells.ToArray(), AllowMultipleOccupancy, BorderWidth, BorderColor);
        }
    }
}
