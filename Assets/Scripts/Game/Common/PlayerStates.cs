using System.Collections.Generic;

namespace Game.Common
{
    public class PlayerStates<T> : IPlayerStates<T>
    {
        private List<IPlayerState<T>> _states;
        public List<IPlayerState<T>> States => _states;

        public IPlayerState<T> GetState(int playerId)
        {
            return _states.Count > playerId ? _states[playerId] : null;
        }

        private IPlayerStateFactory<T> _factory;

        public PlayerStates(IPlayerStateFactory<T> factory)
        {
            _factory = factory;
            _states = new List<IPlayerState<T>>();
        }
        
        public void Init(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _states.Add(new PlayerState<T>(_factory.Create()));
            }
        }

        public void Reset()
        {
            _states.Clear();
        }
    }
}