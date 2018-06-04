using System.Collections.Generic;

namespace GameModel.Instance
{
    public interface IDefinitionInstance<TDefinition>
    {
        TDefinition Definition { get; }
    }
}
