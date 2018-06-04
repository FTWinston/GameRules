namespace GameModel.Definition
{
    public class CellDefinition
    {
        public CellDefinition(string color, string reference = null)
        {
            Reference = reference;
            Color = color;
        }

        public string Reference { get; }
        public string Color { get; }
    }
}
