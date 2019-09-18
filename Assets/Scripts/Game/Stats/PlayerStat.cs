using UnityEngine;

namespace Game.Stats
{
    public class PlayerStat : IPlayerStat
    {
        private Stat _config;
        public Stat Config => _config;

        private float _max;
        public float Max => _max;
        
        private float _currentValue;
        public float Value => _currentValue;

        public event PlayerStatChangeHandler OnValueChange;
        
        public PlayerStat(Stat config)
        {
            _config = config;
        }

        public void AddMax(float value)
        {
            _max += value;
            SetValue(Max);
        }
        
        public void AddValue(float value)
        {
            SetValue(_currentValue + value);
        }

        public void Reset()
        {
            _max = _config.value;
            SetValue(Max);
        }

        private void SetValue(float value)
        {
            _currentValue = Mathf.Clamp(value, 0, Max);
            if (OnValueChange != null) OnValueChange(_config.id, _currentValue);
        }
    }
}