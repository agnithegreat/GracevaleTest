using System.Collections.Generic;
using Game.Common;

namespace Game.Stats
{
    public class PlayerStatsFactory : IPlayerStateFactory<float>
    {
        private readonly Data _config;
        
        public PlayerStatsFactory(Data config)
        {
            _config = config;
        }
        
        public List<float> Create()
        {
            var stats = new List<float>();
            for (var i = 0; i < _config.stats.Length; i++)
            {
                stats.Add(_config.stats[i].value);
            }
            return stats;
        }
    }
}