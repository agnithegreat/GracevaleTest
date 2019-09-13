using System.Collections.Generic;

namespace Game.Common
{
    public class PlayerState<T> : IPlayerState<T>
    {
        private List<T> _state;
        public List<T> State => _state;
        public int Count => _state.Count;

        public T GetValue(int index)
        {
            return _state[index];
        }

        public PlayerState(List<T> state)
        {
            _state = state;
        }

        public void Reset()
        {
            _state = null;
        }
    }
}