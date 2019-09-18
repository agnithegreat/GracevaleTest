using System.Collections.Generic;
using Game.Buffs;
using UnityEngine;
using Random = System.Random;

namespace Game
{
    public class GameModel : IGameModel
    {
        private readonly Data _config;
        private readonly Random _random;

        private PlayerBuffsFactory _playerBuffsFactory;

        private List<PlayerModel> _players;

        public PlayerModel GetPlayer(int id)
        {
            return _players[id];
        }

        public GameModel(Data config)
        {
            _config = config;
            _random = new Random();
            
            _playerBuffsFactory = new PlayerBuffsFactory(_config, _random);
            
            _players = new List<PlayerModel>();
        }

        public void Init(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var player = new PlayerModel(i);
                player.InitStats(_config);
                player.SetTarget((i+1) % count); // выставляем целью следующего по кругу игрока
                _players.Add(player);
            }
        }

        public void Start(bool allowBuffs)
        {
            for (var i = 0; i < _players.Count; i++)
            {
                var player = _players[i];
                player.Reset();
                
                if (allowBuffs)
                {
                    player.InitBuffs(_playerBuffsFactory.Create(), _config);
                }
                
                player.Finalize();
                player.ApplyBuffs();
            }
        }

        public void Attack(int attackerId)
        {
            var attacker = _players[attackerId];
            if (attacker.IsDead) return;
            
            var target = _players[attacker.Target];
            if (target.IsDead) return;

            var hp = target.GetStat(StatsId.LIFE_ID);
            var armor = target.GetStat(StatsId.ARMOR_ID) * 0.01f;
            var damage = attacker.GetStat(StatsId.DAMAGE_ID) * (1f - armor);
            damage = Mathf.Min(damage, hp);
            target.Damage(-damage);
            
            var lifeSteal = attacker.GetStat(StatsId.LIFE_STEAL_ID) * 0.01f;
            attacker.Heal(damage * lifeSteal);
            
            attacker.Attack();
        }
    }
}