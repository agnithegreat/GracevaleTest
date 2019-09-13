using System.Collections.Generic;

namespace Game.Common
{
    public interface IPlayerStateFactory<TState>
    {
        List<TState> Create();
    }
}