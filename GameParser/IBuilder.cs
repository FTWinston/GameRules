namespace GameParser
{
    public interface IBuilder<T>
    {
        bool CanBuild();
        T Build();
    }
}
