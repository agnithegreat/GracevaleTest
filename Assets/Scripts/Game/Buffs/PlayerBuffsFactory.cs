using System;
using System.Collections.Generic;
using Game.Common;
using Tools;

namespace Game.Buffs
{
    public class PlayerBuffsFactory : IPlayerStateFactory<int>
    {
        private readonly Data _config;
        private readonly Random _random;
        
        public PlayerBuffsFactory(Data config, Random random)
        {
            _config = config;
            _random = random;
        }
        
        public List<int> Create()
        {
            var buffsCount = _random.Next(_config.settings.buffCountMin, _config.settings.buffCountMax);
            return RandomizeHelper.Randomize(_config.buffs.Length, buffsCount, _config.settings.allowDuplicateBuffs);
        }
    }
}