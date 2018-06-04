namespace GameModel.Definition
{
    public class Cell
    {
        public Cell(string color, string reference = null)
        {
            Reference = reference;
            Color = color;
        }

        public string Reference { get; }
        public string Color { get; }
    }
}
