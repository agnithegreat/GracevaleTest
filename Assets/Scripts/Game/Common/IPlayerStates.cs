using System.Collections.Generic;

namespace Game.Common
{
    public interface IPlayerStates<TState>
    {
        List<IPlayerState<TState>> States { get; }
        IPlayerState<TState> GetState(int index);
        void Init(int count);
        void Reset();
    }
}