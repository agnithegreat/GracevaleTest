using Game.Buffs;
using Game.Common;
using Game.Stats;
using UnityEngine;
using Random = System.Random;

namespace Game
{
    public class GameLogic
    {
        private readonly Data _config;
        private readonly Random _random;

        private IPlayerStates<float> _playersStats;
        public IPlayerState<float> GetPlayerStats(int playerId)
        {
            return _playersStats.GetState(playerId);
        }

        private IPlayerStates<int> _playersBuffs;
        public IPlayerState<int> GetPlayerBuffs(int playerId)
        {
            return _playersBuffs.GetState(playerId);
        }

        public GameLogic(Data config)
        {
            _config = config;
            _random = new Random();
            
            var statsFactory = new PlayerStatsFactory(_config);
            _playersStats = new PlayerStates<float>(statsFactory);
            
            var buffsFactory = new PlayerBuffsFactory(_config, _random);
            _playersBuffs = new PlayerStates<int>(buffsFactory);
        }

        public void Start(int count, bool allowBuffs)
        {
            _playersStats.Init(count);

            if (allowBuffs)
            {
                _playersBuffs.Init(count);
                
                for (var i = 0; i < count; i++)
                {
                    var buffs = _playersBuffs.GetState(i);
                    for (var j = 0; j < buffs.Count; j++)
                    {
                        var buffId = buffs.GetValue(j);
                        var buff = _config.buffs[buffId];
                        for (var k = 0; k < buff.stats.Length; k++)
                        {
                            var stat = buff.stats[k];
                            _playersStats.GetState(i).State[stat.statId] += stat.value;
                        }
                    }
                }
            }
        }

        public void Attack(int attackerId, int targetId)
        {
            var attacker = _playersStats.GetState(attackerId);
            var target = _playersStats.GetState(targetId);

            var hp = target.GetValue(StatsId.LIFE_ID);
            var armor = target.GetValue(StatsId.ARMOR_ID) * 0.01f;
            var damage = attacker.GetValue(StatsId.DAMAGE_ID) * (1f - armor);
            damage = Mathf.Min(damage, hp);
            target.State[StatsId.LIFE_ID] -= damage;
            
            var lifeSteal = attacker.GetValue(StatsId.LIFE_STEAL_ID) * 0.01f;
            attacker.State[StatsId.LIFE_ID] += damage * lifeSteal;
        }

        public void Reset()
        {
            _playersStats.Reset();
            _playersBuffs.Reset();
        }
    }
}