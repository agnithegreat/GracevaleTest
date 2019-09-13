using System.Collections.Generic;

namespace Game.Common
{
    public interface IPlayerState<T>
    {
        List<T> State { get; }
        int Count { get; }
        T GetValue(int index);
        void Reset();
    }
}