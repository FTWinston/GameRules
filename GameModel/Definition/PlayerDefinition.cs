namespace GameModel.Definition
{
    public class PlayerDefinition
    {
        public PlayerDefinition(bool required, string name, string color)
        {
            Required = required;
            Name = name;
            Color = color;
        }

        public bool Required { get; }
        public string Name { get; }
        public string Color { get; }
    }
}
