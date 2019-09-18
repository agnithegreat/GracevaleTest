using System.Collections.Generic;
using Game.Buffs;
using Game.Stats;

namespace Game
{
    public class PlayerModel : IPlayerModel
    {
        private int _id;
        public int Id => _id;
        
        private int _target;
        public int Target => _target;

        public bool IsDead => _stats[StatsId.LIFE_ID].Value <= 0;
        
        private List<PlayerStat> _stats = new List<PlayerStat>();
        public IEnumerable<IPlayerStat> Stats => _stats;

        public float GetStat(int id)
        {
            return _stats[id].Value;
        }
        
        private List<IPlayerBuff> _buffs = new List<IPlayerBuff>();
        public IEnumerable<IPlayerBuff> Buffs => _buffs;

        public event PlayerAttackHandler AttackHandler;
        public event PlayerHealthChangeHandler HealthChangeHandler;
        public event PlayerHealHandler HealHandler;
        public event PlayerDamageHandler DamageHandler;
        public event PlayerFinalizeHandler FinalizeHandler;

        public PlayerModel(int id)
        {
            _id = id;
        }

        public void InitStats(Data config)
        {
            for (var i = 0; i < config.stats.Length; i++)
            {
                _stats.Add(new PlayerStat(config.stats[i]));
            }

            _stats[StatsId.LIFE_ID].OnValueChange += OnHealthChanged;
        }

        public void InitBuffs(List<int> buffs, Data config)
        {
            for (var i = 0; i < buffs.Count; i++)
            {
                _buffs.Add(new PlayerBuff(config.buffs[i]));
            }
        }

        public void ApplyBuffs()
        {
            for (var i = 0; i < _buffs.Count; i++)
            {
                var buff = _buffs[i].Config;
                for (var k = 0; k < buff.stats.Length; k++)
                {
                    var stat = buff.stats[k];
                    _stats[stat.statId].AddMax(stat.value);
                }
            }
        }

        public void SetTarget(int id)
        {
            _target = id;
        }

        public void Attack()
        {
            AttackHandler?.Invoke();
        }

        public void Damage(float damage)
        {
            var stat = _stats[StatsId.LIFE_ID];
            stat.AddValue(damage);
            DamageHandler?.Invoke((int) damage);
        }
        
        public void Heal(float heal)
        {
            var stat = _stats[StatsId.LIFE_ID];
            stat.AddValue(heal);
            HealHandler?.Invoke((int) heal);
        }

        public void Reset()
        {
            for (var i = 0; i < _stats.Count; i++)
            {
                _stats[i].Reset();
            }
            
            _buffs.Clear();
        }

        public void Finalize()
        {
            FinalizeHandler?.Invoke();
        }

        private void OnHealthChanged(int id, float value)
        {
            HealthChangeHandler?.Invoke(value, _stats[id].Max);
        }
    }
}