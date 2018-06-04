namespace RulesParser
{
    public interface IBuilder<T>
    {
        bool CanBuild();
        T Build();
    }
}
